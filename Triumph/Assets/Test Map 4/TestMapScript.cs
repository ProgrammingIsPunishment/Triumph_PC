using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapScript : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        //Color countryColor = tex.GetPixel(hit.textureCoord * tex.width, pixelUV.y * tex.height);

        // Then, find the country based on the color.
        // You can use ColorUtility.TryParseHtmlString if you want to store an hexadecimal representation of your color in your data file (JSON / Database)      
        // https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html

        // Highlight the real texture with a custom shader
        // This shader would take two textures : the displayed one and the colored one, and a color
        // The color would help you highlight the correct texel. Something like :
        /*
              fixed4 frag (v2f i):SV_Target{
                  fixed4 mainCol=tex2D(_MainTex,i.uv);
                  fixed4 countryCol=tex2D(_ColoredTex,i.uv);
                   if( countryCol == _HighlightCol )
                      Make mainCol brighter
                  return mainCol; 
              }
        */
    }
}
