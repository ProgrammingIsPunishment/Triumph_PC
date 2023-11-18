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
    public Inventory NaturalResourcesInventory { get; set; }
    public List<Holding> AdjacentHoldings { get; set; }
    public VisibilityLevel VisibilityLevel { get; set; }

    [NonSerialized] public HoldingDisplayManager HoldingDisplayManager;

    public Holding(string displayName, int xPosition, int ZPosition, TerrainType terrainType, Inventory naturalResourcesInventory)
    {
        this.DisplayName = displayName;
        this.XPosition = xPosition;
        this.ZPosition = ZPosition;
        this.TerrainType = terrainType;
        this.NaturalResourcesInventory = naturalResourcesInventory;
        this.AdjacentHoldings = new List<Holding>();
    }
}
