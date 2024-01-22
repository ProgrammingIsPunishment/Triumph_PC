using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GoodsTemplate
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public List<Good> Goods { get; set; }

    public GoodsTemplate(string displayName, List<Good> goods)
    {
        this.DisplayName = displayName;
        this.Goods = goods;
    }
}
