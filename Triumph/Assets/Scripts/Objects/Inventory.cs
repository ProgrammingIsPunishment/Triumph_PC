using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// Population's Goods
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CanConsumeGood(string guid, int amount)
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

    public void ConsumeGood(string guid, int amount)
    {
        ResourceItem tempResourceItem = this.ResourceItems.Find(ri => ri.GUID == guid);
        tempResourceItem.Consume(amount);

        //Remove item if it has been fully used up
        if (tempResourceItem.Amount <= 0)
        {
            this.ResourceItems.Remove(tempResourceItem);
        }
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
