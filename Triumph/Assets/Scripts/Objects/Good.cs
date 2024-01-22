using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Good
{
    public GoodType GoodType { get; set; }
    public string ResourceItemGUID { get; set; }
    public int RequiredAmount { get; set; }

    public Good(GoodType goodType, string resourceItemGUID, int requiredAmount)
    {
        this.GoodType = goodType;
        this.ResourceItemGUID = resourceItemGUID;
        this.RequiredAmount = requiredAmount;
    }
}
