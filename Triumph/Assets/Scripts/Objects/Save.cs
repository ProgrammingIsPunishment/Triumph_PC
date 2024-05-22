using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

[Serializable]
public class Save
{
    public int Turn { get; set; }
    public string Name { get; set; }
    public string MapName { get; set; }
    public string PlayerGUID { get; set; }
    public List<Civilization> Civilizations { get; set; } = new List<Civilization>();
    public List<Unit> Units { get; set; } = new List<Unit>();
    public List<Holding> Holdings { get; set; } = new List<Holding>();

    public Save(string name)
    {
        this.Name = name;
    }

    public Save(string name, string mapName)
    {
        this.Name = name;
        this.MapName = mapName;
        this.Turn = 1;
    }

    public string FileName()
    {
        return this.Name + ".save";
    }
}
