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
    public List<Holding> AllHoldings { get; set; }
    public List<ResourceItem> AllResourceItems { get; set; }
    public List<Civilization> AllCivilizations { get; set; }

    public Save(string name)
    {
        this.Name = name;
    }

    public string FileName()
    {
        return this.Name + ".save";
    }
}
