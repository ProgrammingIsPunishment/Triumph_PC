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
    public int ActionPointLimit { get; set; }
    public int ActionPoints { get; set; }
    public InfluentialPerson Commander { get; set; }
    public Inventory Inventory { get; set; }
    public Supply Supply { get; set; }

    [NonSerialized] public UnitDisplayManager UnitDisplayManager;

    public Unit(string displayName, int xPosition, int zPosition, UnitType unitType, int actionPointLimit)
    {
        this.DisplayName = displayName;
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.UnitType = unitType;
        this.Commander = null;
        this.Inventory = new Inventory(InventoryType.UnitSupply,new List<ResourceItem>());
        this.ActionPointLimit = actionPointLimit;
    }

    public void Move(int xPosition, int zPosition)
    {
        this.UnitDisplayManager.transform.localPosition = new Vector3(xPosition*10, 0f, zPosition*10);
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.ActionPoints--;
    }

    public void Gather(ResourceItem gatheredResourceItem)
    {
        List<ResourceItem> gatheredResourceItems = gatheredResourceItem.Breakdown();
        this.Inventory.Add(gatheredResourceItems);
        this.ActionPoints--;
    }

    public void Construct()
    {
        this.ActionPoints--;
    }

    public void CalculateAttrition()
    {
        foreach (Attrition a in this.Supply.Attritions)
        {
            ResourceItem tempResourceItem = this.Inventory.ResourceItems.Find(ri=>ri.GUID == a.ResourceItemGUID);
            if (tempResourceItem.Amount >= a.PerTurnConsumption)
            {
                //There is enouch to increase the attrition
                tempResourceItem.Consume(a.PerTurnConsumption);
                a.Increase();
            }
            else
            {
                //There is not the amount that the unit needs
                if (tempResourceItem.Amount > 0)
                {
                    tempResourceItem.Consume(tempResourceItem.Amount);
                }

                a.Decrease();
            }

            Debug.Log(a.ResourceItemGUID + ": " + a.Amount.ToString());
            Debug.Log(tempResourceItem.GUID + ": " + tempResourceItem.Amount.ToString());
        }
    }

    public void Recover()
    {
        this.ActionPoints = this.ActionPointLimit;
    }
}
