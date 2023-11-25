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
