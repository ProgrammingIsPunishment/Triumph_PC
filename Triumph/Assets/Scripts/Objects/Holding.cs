using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Holding
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public int XPosition { get; set; }
    public int ZPosition { get; set; }
    public TerrainType TerrainType { get; set; }
    public List<ResourceItem> ResourceItems { get; set; }
    public Unit Unit { get; set; }
    public List<string> DiscoveredCivilizationGUIDs { get; set; }
    public List<string> AdjacentHoldingGUIDs { get; set; }

    [NonSerialized] public HoldingManager HoldingManager;

    public Holding(string displayName, int xPosition, int ZPosition, TerrainType terrainType, List<ResourceItem> resourceItems)
    {
        this.DisplayName = displayName;
        this.XPosition = xPosition;
        this.ZPosition = ZPosition;
        this.TerrainType = terrainType;
        this.ResourceItems = resourceItems;
        this.Unit = null;
        this.DiscoveredCivilizationGUIDs = new List<string>();
        this.AdjacentHoldingGUIDs = new List<string>();
    }
}
