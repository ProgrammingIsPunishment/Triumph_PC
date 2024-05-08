using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Holding
{
    [SerializeField] public string GUID { get; set; }
    [SerializeField] public string Name { get; set; }
    [SerializeField] public int XPosition { get; set; }
    [SerializeField] public int ZPosition { get; set; }
    [SerializeField] public TerrainType TerrainType { get; set; }
    [SerializeField] public List<Holding> AdjacentHoldings { get; set; }

    [NonSerialized] public HoldingManager CoupledHoldingDisplay = null;

    public Holding(string guid, string name, int xPosition, int zPosition, TerrainType terrainType)
    {
        this.GUID = guid;
        this.Name = name;
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.TerrainType = terrainType;
        this.AdjacentHoldings = new List<Holding>();
    }

    public void UpdateVisibility()
    {
        this.CoupledHoldingDisplay.ShowDiscovered();
    }
}
