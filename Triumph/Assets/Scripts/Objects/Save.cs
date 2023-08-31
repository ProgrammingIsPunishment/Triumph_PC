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
    public List<Civilization> AllCivilizations { get; set; }
    public List<InfluentialPerson> AllInfluentialPeople { get; set; }
    public List<Unit> AllUnits { get; set; }

    public Save(string name, string mapName, List<Holding> allHoldings, List<ResourceItem> allResourceItems, List<InfluentialPerson> allInfluentialPeople)
    {
        this.Name = name;
        this.MapName = mapName;
        this.AllHoldings = allHoldings;
        this.AllResourceItems = allResourceItems;
        this.AllInfluentialPeople = allInfluentialPeople;
    }

    public string FileName()
    {
        return this.Name + ".save";
    }
}
