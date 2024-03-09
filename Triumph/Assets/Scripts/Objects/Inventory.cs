using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public InventoryType InventoryType { get; set; }
    public List<ResourceItem> ResourceItems { get; set; }
    private List<ResourceItemType> AllowedResourceItemTypes { get; set; }

    public Inventory(InventoryType inventoryType, List<ResourceItem> resourceItems)
    {
        this.InventoryType = inventoryType;
        this.ResourceItems = resourceItems;

        switch (inventoryType)
        {
            case InventoryType.NaturalResources:
                this.AllowedResourceItemTypes = new List<ResourceItemType>() { ResourceItemType.Foliage, ResourceItemType.Fauna };
                break;
        }
    }

    public void RemoveResourceItem(string guid, int amount)
    {
        List<ResourceItem> tempResourceItems = this.ResourceItems.Where(ri => ri.GUID == guid).ToList();

        foreach (ResourceItem ri in tempResourceItems)
        {
            if (ri.Amount >= amount)
            {
                ri.Amount -= amount;
                amount = 0;
            }
            else
            {
                amount -= ri.Amount;
                ri.Amount = 0;
            }

            //Remove item if it has been fully used up
            if (ri.Amount <= 0)
            {
                this.ResourceItems.Remove(ri);
            }

            if (amount <= 0)
            {
                return;
            }
        }
    }

    /// <summary>
    /// Population's Goods
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool HasGood(string guid, int amount)
    {
        bool result = false;

        foreach (ResourceItem ri in this.ResourceItems)
        {
            if (ri.GUID == guid && ri.Amount >= amount)
            {
                return true;
            }
        }

        return result;
    }

    //public void ConsumeGood(string guid, int amount)
    //{
    //    ResourceItem tempResourceItem = this.ResourceItems.Find(ri => ri.GUID == guid);
    //    tempResourceItem.Consume(amount);

    //    //Remove item if it has been fully used up
    //    if (tempResourceItem.Amount <= 0)
    //    {
    //        this.ResourceItems.Remove(tempResourceItem);
    //    }
    //}

    private bool HasResourceAndQuantity(string resourceGUID, int resourceQuantity)
    {
        bool result = false;

        int workingQuantity = 0;
        bool hasResource = false;

        foreach (ResourceItem ri in this.ResourceItems)
        {
            if (ri.GUID == resourceGUID)
            {
                hasResource = true;
                workingQuantity += ri.Amount;
            }
        }

        if (hasResource && (resourceQuantity <= workingQuantity))
        {
            result = true;
        }

        return result;
    }

    public bool HasResourcesForConstruction(List<Tuple<string,int>> requiredComponents)
    {
        bool result = false;

        List<bool> meetsRequirement = new List<bool>();
        foreach (Tuple<string, int> rc in requiredComponents)
        {
            meetsRequirement.Add(this.HasResourceAndQuantity(rc.Item1,rc.Item2));
        }

        if (!meetsRequirement.Contains(false))
        {
            result = true;
        }

        return result;
    }

    public bool ContainsItem(string guid)
    {
        bool result = false;

        foreach (ResourceItem ri in this.ResourceItems)
        {
            if (ri.GUID == guid)
            {
                return true;
            }
        }

        return result;
    }

    public void Add(List<ResourceItem> resourceItems)
    {
        foreach (ResourceItem ri in resourceItems)
        {
            if (this.ContainsItem(ri.GUID))
            {
                //Already has an item of this type
                //try to add to the stack
                ResourceItem tempResourceItem = this.ResourceItems.Find(r=>r.GUID == ri.GUID);
                tempResourceItem.AddToStack(ri.Amount);
            }
            else
            {
                //New item of this type
                this.ResourceItems.Add(ri);
            }
        }
    }
}
