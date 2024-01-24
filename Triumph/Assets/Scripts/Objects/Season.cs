using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Season
{
    public string Name { get; set; }
    public int Order { get; set; }
    public string IconFileName { get; set; }

    public Season(string name, int order, string iconFileName)
    {
        this.Name = name;
        this.Order = order;
        this.IconFileName = iconFileName;
    }
}
