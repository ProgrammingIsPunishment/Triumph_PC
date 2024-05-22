using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilization
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public Color Color { get; set; }
    public List<Holding> Holdings { get; set; }
    public List<Unit> Units { get; set; }

    public Civilization(string guid, string displayName, Color color)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.Color = color;
        this.Holdings = new List<Holding>();
        this.Units = new List<Unit>();
        this.Dispatches = new List<Dispatch>();
    }
}
