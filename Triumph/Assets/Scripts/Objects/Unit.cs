using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unit
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public int XPosition { get; set; }
    public int ZPosition { get; set; }
    public UnitType UnitType { get; set; }
    public InfluentialPerson Commander { get; set; }
    public Inventory SupplyInventory { get; set; }

    [NonSerialized] public UnitDisplayManager UnitDisplayManager;

    public Unit(string displayName, int xPosition, int zPosition, UnitType unitType)
    {
        this.DisplayName = displayName;
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.UnitType = unitType;
        this.Commander = null;
        this.SupplyInventory = new Inventory(InventoryType.UnitSupply,new List<ResourceItem>());
    }

    public void Move(int xPosition, int zPosition)
    {
        this.UnitDisplayManager.transform.localPosition = new Vector3(xPosition*10, 0f, zPosition*10);
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
    }
}
