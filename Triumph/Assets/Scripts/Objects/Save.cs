using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    public string Name { get; set; }
    public string MapName { get; set; }
    public List<Holding> AllHoldings { get; set; }
    public List<ResourceItem> AllResourceItems { get; set; }

    public Save(string name, string mapName, List<Holding> allHoldings, List<ResourceItem> allResourceItems)
    {
        this.Name = name;
        this.MapName = mapName;
        this.AllHoldings = allHoldings;
        this.AllResourceItems = allResourceItems;
    }
}
