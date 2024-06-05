using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilization
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public Color Color { get; set; }
    public AICivilizationProfile AICivilizationProfile { get; set; } = new AICivilizationProfile();

    public Civilization(string guid, string displayName, Color color)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.Color = color;
    }
}
