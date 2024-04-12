using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory
{
    public string Name { get; set; }

    public Territory(string name)
    {
        this.Name = name;
    }
}

public class MapPicker : MonoBehaviour
{
    // Assign your quad's collider here.
    public MeshCollider mapShape;

    // Assign your colour-coded territory map here.
    public Texture2D colourMap;

    // We'll use this to quickly find a territory by its colour.
    Dictionary<Color32, Territory> _territoryLookup;

    private void Start()
    {
        // Fetch the pixel data of the texture as a big block we can iterate through quickly.
        var pixels = colourMap.GetPixels32();

        // Prep our lookup table. For efficiency, tell it how many territories you expect.
        _territoryLookup = new Dictionary<Color32, Territory>(2000);

        // Quick & dirty: we'll generate a new territory for each unique colour we find.
        // Later, you can instead populate your lookup table from ScriptableObjects if needed.
        foreach (var pixel in pixels)
        {
            if (_territoryLookup.ContainsKey(pixel))
                continue;

            _territoryLookup.Add(pixel, new Territory($"Territory: {pixel}"));
        }
    }

    void Update()
    {
        // If no click this frame, abort. Nothing to do.
        if (!Input.GetMouseButtonDown(0))
            return;

        // Cast a ray against the map to see if/where it hits.
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!mapShape.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
            return;  // If we missed the map entirely, abort.

        // Map the hit point on the quad to a pixel coordinate in our texture.
        var mapSize = new Vector2(colourMap.width, colourMap.height);
        var hitPoint = Vector2Int.RoundToInt(Vector2.Scale(hit.textureCoord, mapSize));

        // Fetch the colour of the pixel we hit.
        var colour = (Color32)colourMap.GetPixel(hitPoint.x, hitPoint.y);

        // Now we can look up our territory using that colour.
        var territory = _territoryLookup[colour];

        /* TODO: do something with the territory we have clicked */

        Debug.Log(territory.Name);
    }
}
