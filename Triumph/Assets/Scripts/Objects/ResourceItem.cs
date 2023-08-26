using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceItem
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public ResourceItemType ResourceItemType { get; set; }
    public int Amount { get; set; }
    public int StackLimit { get; set; }
    public List<Tuple<string, int>> ResourceItemComponents { get; set; }


    public ResourceItem(string guid, string displayName, ResourceItemType resourceItemType, int stackLimit, List<Tuple<string, int>> resourceItemComponents)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.ResourceItemType = resourceItemType;
        this.Amount = 0;
        this.StackLimit = stackLimit;
        this.ResourceItemComponents = resourceItemComponents;
    }

    public ResourceItem CreateInstance()
    {   
        return new ResourceItem(this.GUID,this.DisplayName,this.ResourceItemType,this.StackLimit,this.ResourceItemComponents);
    }

    public void AddToStack(int amount)
    {
        this.Amount += amount;
        if (this.Amount > this.StackLimit) { this.Amount = this.StackLimit; }
    }
}
