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
}
