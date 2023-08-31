using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Civilization
{
    public string Name { get; set; }
    public List<string> DiscoveredHoldingGUIDs { get; set; }
    public List<string> InfluentialPeopleGUIDs { get; set; }
    public List<string> LeaderGUIDs { get; set; }

    public Civilization(string name, List<string> discoveredHoldingGUIDs,List<string> influentialPeopleGUIDs, List<string> leaderGUIDs)
    {
        this.Name = name;
        this.DiscoveredHoldingGUIDs = discoveredHoldingGUIDs;
        this.InfluentialPeopleGUIDs = influentialPeopleGUIDs;
        this.LeaderGUIDs = leaderGUIDs;
    }
}
