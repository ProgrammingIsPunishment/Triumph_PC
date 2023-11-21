using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attrition
{
    public string ResourceItemGUID { get; set; }
    public int Maximum { get; set; }
    public int Amount { get; set; }
    public int PerTurnLoss { get; set; }

    public Attrition(string resourceItemGUID, int maximum, int perTurnLoss)
    {
        this.ResourceItemGUID = resourceItemGUID;
        this.Maximum = maximum;
        this.PerTurnLoss = perTurnLoss;
    }
}
