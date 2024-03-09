using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pop
{
    //public int People { get; set; }
    public string GUID { get; set; }
    public GoodsTemplate GoodsTemplate { get; set; }
    public List<Effect> Effects { get; set; }
    public float Happiness { get; set; }
    public float Necessities { get; set; }

    private bool metNecessityRequirements;

    public Pop(GoodsTemplate goodsTemplate)
    {
        //this.People = amount;
        this.GoodsTemplate = goodsTemplate;
        this.Effects = new List<Effect>();
        this.Happiness = 10;
        this.Necessities = 10;
        this.metNecessityRequirements = true;
    }

    //public void Settle(int amount)
    //{
    //    //this.People += amount;
    //}

    public void Consume(Inventory inventory)
    {
        this.metNecessityRequirements = true;

        foreach (Good g in this.GoodsTemplate.Goods)
        {
            if (inventory.HasGood(g.ResourceItemGUID, g.RequiredAmount))
            {
                //Able to consume good
                inventory.RemoveResourceItem(g.ResourceItemGUID, g.RequiredAmount);
                //if (g.GoodType == GoodType.Necessity){}
            }
            else
            {
                //Unable to consume good
                if (g.GoodType == GoodType.Necessity)
                {
                    this.metNecessityRequirements = false;
                }
            }
        }
    }

    public bool HasEffect(string guid)
    {
        bool result = false;

        Effect tempEffect = this.Effects.Find(e=>e.GUID==guid);

        if (this.Effects.Contains(tempEffect))
        {
            return true;
        }

        return result;
    }

    public void RemoveEffect(string guid)
    {
        Effect tempEffectToRemove = this.Effects.Find(e=>e.GUID == guid);
        this.Effects.Remove(tempEffectToRemove);
    }

    public void AddEffect(string guid)
    {
        if (!this.HasEffect(guid))
        {
            Effect tempEffectToAdd = Oberkommando.SAVE.AllEffects.Find(e => e.GUID == guid).CreateInstance();
            this.Effects.Add(tempEffectToAdd);
        }
    }

    //public void AddStackableEffect(string guid)
    //{
    //    Effect tempEffectToAdd = Oberkommando.SAVE.AllEffects.Find(e => e.GUID == guid).CreateInstance();
    //    this.Effects.Add(tempEffectToAdd);
    //}
}
