using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Population
{
    public int Amount { get; set; }
    public GoodsTemplate GoodsTemplate { get; set; }
    public List<Effect> Effects { get; set; }
    public float Happiness { get; set; }
    public float Necessities { get; set; }

    private bool metNecessityRequirements;

    public Population(int amount, GoodsTemplate goodsTemplate)
    {
        this.Amount = amount;
        this.GoodsTemplate = goodsTemplate;
        this.Effects = new List<Effect>();
        this.Happiness = 10;
        this.Necessities = 10;
        this.metNecessityRequirements = true;
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

    public void CalculateConsumption(Inventory storageInventory)
    {
        if (this.Amount > 0)
        {
            foreach (Good g in this.GoodsTemplate.Goods)
            {
                if (storageInventory.CanConsumeGood(g.ResourceItemGUID, g.RequiredAmount))
                {
                    //Can consume good
                    storageInventory.ConsumeGood(g.ResourceItemGUID, g.RequiredAmount);
                    if (g.GoodType == GoodType.Necessity)
                    {
                        this.metNecessityRequirements = true;
                    }
                }
                else
                {
                    //Cannot consume good
                    if (g.GoodType == GoodType.Necessity)
                    {
                        this.metNecessityRequirements = false;
                    }
                }
            }
        }
    }

    public void DetermineTurnEffects()
    {
        if (this.Amount > 0)
        {
            if (this.Happiness >= 7)
            {
                this.AddEffect("fulloflife");
            }
            else 
            { 
                this.RemoveEffect("fulloflife"); 
            }

            if (this.metNecessityRequirements)
            {
                this.AddEffect("sated");
            }
            else
            {
                this.RemoveEffect("sated");

                if (!this.HasEffect("starving"))
                {
                    if (!this.HasEffect("deprived"))
                    {
                        this.AddEffect("deprived");
                    }
                    else
                    {
                        this.Effects.Find(e => e.GUID == "deprived").Value += 1;
                    }
                }
            }
        }
    }

    public void DetermineSeasonalEffects()
    {
        if (this.Amount > 0)
        {
            if (this.HasEffect("deprived"))
            {
                this.RemoveEffect("deprived");
                this.AddEffect("starving");
            }
            else if (this.HasEffect("sated"))
            {
                this.RemoveEffect("starving");
            }
        }
    }

    public void ProcessTurnEffects()
    {
        float workingHappiness = 10;
        float workingNecessities  = 10;

        if (this.Amount > 0)
        {
            foreach (Effect e in this.Effects)
            {
                switch (e.EffectType)
                {
                    case EffectType.Happiness: workingHappiness = e.ProcessValue(workingHappiness); break;
                    case EffectType.Neccessities: workingNecessities  = e.ProcessValue(workingNecessities ); break;
                    case EffectType.Population: /*Do nothing?*/ break;
                }
            }

            if (workingHappiness > 10) { workingHappiness = 10; }
            else if (workingHappiness < 0) { workingHappiness = 0; }

            if (workingNecessities  > 10) { workingNecessities  = 10; }
            else if (workingNecessities  < 0) { workingNecessities  = 0; }

            this.Happiness = workingHappiness;
            this.Necessities = workingNecessities ;
        }
    }

    public void ProcessSeasonalEffects()
    {
        if (this.Amount > 0)
        {
            foreach (Effect e in this.Effects)
            {
                switch (e.GUID)
                {
                    case "fulloflife":
                        this.CalculatePopulationGrowth(e.Value);
                        break;
                    case "starving":
                        this.CalculateStarvationDeath(e.Value);
                        break;
                    default:
                        //Do nothing...
                        break;
                }
            }
        }
    }

    private void CalculatePopulationGrowth(float percentGain)
    {
        float workingPopulation = this.Amount;
        float amountToGain = (float)Math.Ceiling(this.Amount * percentGain);
        workingPopulation += amountToGain;

        this.Amount = (int)workingPopulation;
    }

    private void CalculateStarvationDeath(float percentLoss)
    {
        float workingRemainingPopulation = this.Amount;
        float amountToDie = (float)Math.Ceiling(this.Amount * percentLoss);
        workingRemainingPopulation -= amountToDie;

        this.Amount = (int)workingRemainingPopulation;
    }
}
