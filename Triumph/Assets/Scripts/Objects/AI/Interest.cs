using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Interest
{
    public Holding Holding { get; private set; }

    public int TerritorialExpansionWeight { get; set; }
    public int UnitProximityWeight { get; set; }

    public Interest(Holding holding)
    {
        this.Holding = holding;
        this.TerritorialExpansionWeight = 0;
        this.UnitProximityWeight = 0;
    }

    public void Reset()
    {
        this.TerritorialExpansionWeight = 0;
        this.UnitProximityWeight = 0;
    }
}
