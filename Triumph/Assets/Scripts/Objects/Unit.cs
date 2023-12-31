using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unit
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public UnitType UnitType { get; set; }
    public int ActionPoints { get; set; }
    public InfluentialPerson Commander { get; set; }
    public Inventory SupplyInventory { get; set; }

    public Unit(string guid, string displayName, UnitType unitType, InfluentialPerson commander, Inventory supplyInventory)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.UnitType = unitType;
        this.Commander = commander;
        this.SupplyInventory = supplyInventory;
        this.ActionPoints = 2;
    }

    public void Move()
    {
        this.ActionPoints -= 1;
    }

    public void Gather(Inventory holdingInventory, GatherType gatherType)
    {
        this.SupplyInventory.Add(holdingInventory.Gather(gatherType));
    }

    public void RefreshActionPoints()
    {
        this.ActionPoints = 2;
    }
}
