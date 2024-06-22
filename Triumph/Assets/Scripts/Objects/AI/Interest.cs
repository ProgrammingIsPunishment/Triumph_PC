using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Interest
{
    public Holding Holding { get; private set; }

    public int TerritorialExpansionWeight { get; set; }

    public Interest(Holding holding)
    {
        this.Holding = holding;
        this.TerritorialExpansionWeight = 0;
    }
}
