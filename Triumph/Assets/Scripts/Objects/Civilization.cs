using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Civilization
{
    public string GUID { get; set; }
    public string Name { get; set; }
    public List<InfluentialPerson> InfluentialPeople { get; set; }
    public List<Unit> Units { get; set; }
    public List<Holding> ExploredHoldings { get; set; }
    public InfluentialPerson Leader { get; set; }

    public Civilization(string guid, string name)
    {
        this.GUID = guid;
        this.Name = name;
        this.InfluentialPeople = new List<InfluentialPerson>();
        this.Units = new List<Unit>();
        this.ExploredHoldings = new List<Holding>();
        this.Leader = null;
    }
}
