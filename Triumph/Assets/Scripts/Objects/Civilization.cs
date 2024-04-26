using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilization
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }

    public Civilization(string guid, string displayName)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
    }
}
