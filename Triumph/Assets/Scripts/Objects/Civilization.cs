using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Civilization
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public Color Color { get; set; }
    public CivilizationAI CivilizationAI { get; set; }
    public int Coins { get; set; }
    public List<UnitTemplate> UnitTemplates { get; set; }

    public Civilization(string guid, string displayName, Color color, int coins)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.Color = color;
        this.CivilizationAI = new CivilizationAI(this);
        this.Coins = coins;
        this.UnitTemplates = new List<UnitTemplate>();
    }

    public UnitTemplate UnitTemplateByGUID(string guid)
    {
        return this.UnitTemplates.FirstOrDefault(ut=>ut.GUID == guid);
    }
}
