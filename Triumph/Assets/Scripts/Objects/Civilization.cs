using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Civilization
{
    public string Name { get; set; }
    public List<string> DiscoveredHoldingGUIDs { get; set; }

    public Civilization(string name, List<string> discoveredHoldingGUIDs)
    {
        this.Name = name;
        this.DiscoveredHoldingGUIDs = discoveredHoldingGUIDs;
    }
}
