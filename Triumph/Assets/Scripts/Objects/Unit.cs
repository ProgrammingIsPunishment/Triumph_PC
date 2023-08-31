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
    public string CommanderGUID { get; set; }
    public string HoldingGUID { get; set; }

    public Unit(string guid, string displayName, UnitType unitType, string commanderGUID, string holdingGUID)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.UnitType = unitType;
        this.CommanderGUID = commanderGUID;
        this.HoldingGUID = holdingGUID;
    }
}
