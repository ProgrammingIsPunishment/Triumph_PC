using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InfluentialPerson
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }

    public InfluentialPerson(string guid,string displayName)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
    }
}
