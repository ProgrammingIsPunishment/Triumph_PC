using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Civilization
{
    public string GUID { get; set; }
    public string Name { get; set; }
    public List<string> DiscoveredHoldingGUIDs { get; set; }
    public List<InfluentialPerson> InfluentialPeople { get; set; }

    public Civilization(string guid, string name)
    {
        this.GUID = guid;
        this.Name = name;
        this.DiscoveredHoldingGUIDs = new List<string>();
        this.InfluentialPeople = new List<InfluentialPerson>();
    }
}
