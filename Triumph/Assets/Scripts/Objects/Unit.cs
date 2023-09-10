using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unit
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public UnitType UnitType { get; set; }
    public InfluentialPerson Commander { get; set; }

    public Unit(string guid, string displayName, UnitType unitType, InfluentialPerson commander)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.UnitType = unitType;
        this.Commander = commander;
    }
}
