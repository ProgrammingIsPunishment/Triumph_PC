using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private int width;
    private int height;

    private Color32[] remapArr;
    private Texture2D paletteTexture = null;

    private Color32 previousColor;
    //bool selectAny = false;

    private Texture2D mainTexture = null;
    private List<Color32> allColors = new List<Color32>();

    private XMLController xmlController = new XMLController();

    List<Tuple<string, Color32>> territories;

    // Start is called before the first frame update
    void Start()
    {
        var material = GetComponent<Renderer>().material;
        this.mainTexture = material.GetTexture("_MainTex") as Texture2D;
        //var mainTex = material.GetTexture("_MainTex") as Texture2D;
        var mainArr = this.mainTexture.GetPixels32();

        this.territories = this.xmlController.LoadMapFile("Obsidian");

        //this.allColors.AddRange(mainArr.ToList());

        this.width = this.mainTexture.width;
        this.height = this.mainTexture.height;

        var main2remap = new Dictionary<Color32, Color32>();
        this.remapArr = new Color32[mainArr.Length];
        int idx = 0;
        for (int i = 0; i < mainArr.Length; i++)
        {
            var mainColor = mainArr[i];
            if (!main2remap.ContainsKey(mainColor))
            {
                var low = (byte)(idx % 256);
                var high = (byte)(idx / 256);
                main2remap[mainColor] = new Color32(low, high, 0, 255);
                idx++;


                this.allColors.Add(mainColor);
            }
            var remapColor = main2remap[mainColor];
            this.remapArr[i] = remapColor;
        }

        var paletteArr = new Color32[256 * 256];
        for (int i = 0; i < paletteArr.Length; i++)
        {
            paletteArr[i] = new Color32(255, 255, 255, 255);
        }

        var remapTex = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false);
        remapTex.filterMode = FilterMode.Point;
        remapTex.SetPixels32(this.remapArr);
        remapTex.Apply(false);
        material.SetTexture("_RemapTex", remapTex);

        this.paletteTexture = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        this.paletteTexture.filterMode = FilterMode.Point;
        this.paletteTexture.SetPixels32(paletteArr);
        this.paletteTexture.Apply(false);
        material.SetTexture("_PaletteTex", paletteTexture);

        this.GenerateXML();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            var p = hitInfo.point;
            int x = (int)Mathf.Floor(p.x) + this.width / 2;
            int y = (int)Mathf.Floor(p.y) + this.height / 2;

            Color32 clickedProvinceColor = this.mainTexture.GetPixel(x, y);

            var remapColor = remapArr[x + y * this.width];

            if (!this.previousColor.Equals(remapColor))
            {
                Tuple<string, Color32> tempTerritory = this.territories.Find(t => t.Item2.Equals(clickedProvinceColor));

                if (tempTerritory.Item1 != "none")
                {
                    this.paletteTexture.SetPixel(this.previousColor[0], this.previousColor[1], new Color32(255, 255, 255, 255));
                    this.paletteTexture.SetPixel(remapColor[0], remapColor[1], new Color32(0, 0, 0, 255));
                    this.previousColor = remapColor;
                    this.paletteTexture.Apply(false);
                }
                else
                {
                    this.paletteTexture.SetPixel(this.previousColor[0], this.previousColor[1], new Color32(255, 255, 255, 255));
                    this.paletteTexture.Apply(false);
                }

                Debug.Log(tempTerritory.Item1);
            }

            //if (!selectAny || !prevColor.Equals(remapColor))
            //{
            //    if (selectAny)
            //    {
            //        changeColor(prevColor, new Color32(255, 255, 255, 255));
            //    }
            //    selectAny = true;
            //    prevColor = remapColor;
            //    changeColor(remapColor, new Color32(50, 0, 255, 255));
            //    paletteTex.Apply(false);
            //}

            //Debug.Log($"X:{x} Y:{y}");
        }
    }

    void changeColor(Color32 remapColor, Color32 showColor)
    {
        int xp = remapColor[0];
        int yp = remapColor[1];

        paletteTexture.SetPixel(xp, yp, showColor);
        Debug.ClearDeveloperConsole();
        //Debug.Log($"X:{xp} Y:{yp}");
    }

    private void GenerateXML()
    {
        string workingXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<map>\n\t<territories>\n";

        foreach (Color32 c in this.allColors)
        {
            workingXML += $"\t\t<territory r=\"{c.r}\" g=\"{c.g}\" b=\"{c.b}\" name=\"\"></territory>\n";
        }

        workingXML += "\t</territories>\n</map>";

        Debug.Log(workingXML);
    }
}
