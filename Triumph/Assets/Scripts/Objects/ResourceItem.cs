using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceItem
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public string IconFileName { get; set; }
    public string ModelFileName { get; set; }
    public ResourceItemType ResourceItemType { get; set; }
    public int Amount { get; set; }
    public int StackLimit { get; set; }
    public List<Tuple<string, int>> ResourceItemComponents { get; set; }

    [NonSerialized] public InventorySlotView InventorySlotView;


    public ResourceItem(string guid, string displayName, string iconFileName, ResourceItemType resourceItemType, int stackLimit, List<Tuple<string, int>> resourceItemComponents, string modelFileName)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.IconFileName = iconFileName;
        this.ResourceItemType = resourceItemType;
        this.Amount = 0;
        this.StackLimit = stackLimit;
        this.ResourceItemComponents = resourceItemComponents;
        this.ModelFileName = modelFileName;
    }

    public ResourceItem CreateInstance()
    {   
        return new ResourceItem(this.GUID, this.DisplayName, this.IconFileName, this.ResourceItemType,this.StackLimit,this.ResourceItemComponents, this.ModelFileName);
    }

    public void AddToStack(int amount)
    {
        this.Amount += amount;
        if (this.Amount > this.StackLimit) { this.Amount = this.StackLimit; }
    }

    public List<ResourceItem> Breakdown()
    {
        List<ResourceItem> result = new List<ResourceItem>();

        foreach (Tuple<string, int> ric in this.ResourceItemComponents)
        {
            ResourceItem tempResourceItem = Oberkommando.SAVE.AllResourceItems.Find(ri => ri.GUID == ric.Item1).CreateInstance();
            tempResourceItem.AddToStack(ric.Item2);
            result.Add(tempResourceItem);
        }

        this.Amount--;

        return result;
    }
}
