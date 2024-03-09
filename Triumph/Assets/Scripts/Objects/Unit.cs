using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Population Population { get; set; }

    [NonSerialized] public UnitDisplayManager UnitDisplayManager;

    public Unit(string displayName, int xPosition, int zPosition, UnitType unitType, int actionPointLimit, Population population)
    {
        this.DisplayName = displayName;
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.UnitType = unitType;
        this.Commander = null;
        this.Inventory = new Inventory(InventoryType.UnitSupply,new List<ResourceItem>());
        this.ActionPointLimit = actionPointLimit;
        this.Population = population;
    }

    public bool Move(Holding holding, int xPosition, int zPosition)
    {
        bool unitMoved = false;

        if (holding.HasPassableTerrain())
        {
            this.UnitDisplayManager.transform.localPosition = new Vector3(xPosition * 10, 0f, zPosition * 10);
            this.XPosition = xPosition;
            this.ZPosition = zPosition;
            this.ActionPoints--;
            unitMoved = true;
        }

        return unitMoved;
    }

    public void Gather(ResourceItem gatheredResourceItem)
    {
        List<ResourceItem> gatheredResourceItems = gatheredResourceItem.Breakdown();
        this.Inventory.Add(gatheredResourceItems);
        this.ActionPoints--;
    }

    public void Build(List<Tuple<string,int>> componentsToRemove)
    {
        foreach (Tuple<string, int> ctr in componentsToRemove)
        {
            this.Inventory.RemoveResourceItem(ctr.Item1, ctr.Item2);
        }
        this.ActionPoints--;
    }

    public void Labor()
    {
        this.ActionPoints--;
    }

    public void Settle()
    {
        this.ActionPoints--;
        this.Population.Pops.Clear();
    }

    public void CalculateAttrition()
    {
        foreach (Attrition a in this.Supply.Attritions)
        {
            //if (this.Population > 0)
            //{
            //    ResourceItem tempResourceItem = this.Inventory.ResourceItems.Find(ri => ri.GUID == a.ResourceItemGUID);

            //    if (tempResourceItem != null)
            //    {
            //        if (tempResourceItem.Amount >= a.PerTurnConsumption)
            //        {
            //            //There is enouch to increase the attrition
            //            tempResourceItem.Consume(a.PerTurnConsumption);
            //            a.Increase();
            //        }
            //        else
            //        {
            //            //There is not the amount that the unit needs
            //            if (tempResourceItem.Amount > 0)
            //            {
            //                tempResourceItem.Consume(tempResourceItem.Amount);
            //            }

            //            a.Decrease();
            //        }
            //    }
            //}
        }
    }

    public void Recover()
    {
        this.ActionPoints = this.ActionPointLimit;
    }

    public bool HasPeople()
    {
        bool result = false;

        //if (this.Population >= 1) { result = true; }

        return result;
    }

    public bool HasRequiredResources(List<Tuple<ResourceItem,int>> requiredResources)
    {
        bool result = true;

        List<ResourceItem> unitResourceItems = this.Inventory.ResourceItems;

        for (int i = 0; i < requiredResources.Count; i++)
        {
            ResourceItem tempRequiredResourceItem = requiredResources[i].Item1;
            int tempRequiredResourceAmount = requiredResources[i].Item2;

            List<ResourceItem> tempUnitResourceItems = unitResourceItems.Where(ri => ri.GUID == tempRequiredResourceItem.GUID).ToList();

            if (tempUnitResourceItems.Count >= 1)
            {
                if (tempUnitResourceItems[0].Amount < tempRequiredResourceAmount)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
        }

        return result;
    }
}
