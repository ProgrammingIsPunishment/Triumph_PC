using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public List<Attrition> Attritions { get; set; }

    public Supply(string guid, string displayName, List<Attrition> attritions)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.Attritions = attritions;
    }

    public Supply CreateInstance()
    {
        return new Supply(this.GUID, this.DisplayName, this.Attritions);
    }
}
