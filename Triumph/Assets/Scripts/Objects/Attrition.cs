using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attrition
{
    public string ResourceItemGUID { get; set; }
    public int Maximum { get; set; }
    public int Amount { get; set; }
    public int PerTurnConsumption { get; set; }

    public Attrition(string resourceItemGUID, int maximum, int perTurnConsumption)
    {
        this.ResourceItemGUID = resourceItemGUID;
        this.Maximum = maximum;
        this.PerTurnConsumption = perTurnConsumption;
    }

    public void Increase()
    {
        this.Amount++;
        if (this.Amount > this.Maximum)
        {
            this.Amount = this.Maximum;
        }
    }

    public void Decrease()
    {
        this.Amount--;
        if (this.Amount < 0)
        {
            this.Amount = 0;
        }
    }
}
