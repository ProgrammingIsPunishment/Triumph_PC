using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    public string Name { get; set; }
    public string MapName { get; set; }
    public int Turn { get; set; }
    public Season Season { get; set; }
    public List<Holding> AllHoldings { get; set; }
    public List<Unit> AllUnits { get; set; }
    public List<ResourceItem> AllResourceItems { get; set; }
    public List<Building> AllBuildings { get; set; }
    public List<Civilization> AllCivilizations { get; set; }
    public List<Effect> AllEffects { get; set; }
    public List<Attribute> AllAttributes { get; set; }
    public List<Season> AllSeasons { get; set; }

    public Save(string name)
    {
        this.Name = name;
    }

    public string FileName()
    {
        return this.Name + ".save";
    }
}
