using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AIProfile
{
    public int TerritorialExpansionDesire { get; set; }

    public List<Interest> Interests { get; set; }

    public AIProfile()
    {
        this.TerritorialExpansionDesire = 1;
        this.Interests = new List<Interest>();
    }
}
