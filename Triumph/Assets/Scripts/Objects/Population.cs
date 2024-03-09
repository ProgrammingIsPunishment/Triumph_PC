using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population
{
    public List<Pop> Pops;

    public Population()
    {
        this.Pops = new List<Pop>();
    }

    public List<GoodsTemplate> GetGoodsTemplates()
    {
        List<GoodsTemplate> result = new List<GoodsTemplate>();

        foreach (Pop p in this.Pops)
        {
            result.Add(p.GoodsTemplate);
        }

        return result;
    }

    public void Settle(List<Pop> pops)
    {
        this.Pops.AddRange(pops);
    }

    public void DetermineTurnEffects()
    {
        //if (this.People > 0)
        //{
        //    if (this.Happiness >= 7)
        //    {
        //        this.AddEffect("fulloflife");
        //    }
        //    else 
        //    { 
        //        this.RemoveEffect("fulloflife"); 
        //    }

        //    if (this.metNecessityRequirements)
        //    {
        //        this.AddEffect("sated");
        //    }
        //    else
        //    {
        //        this.RemoveEffect("sated");

        //        if (!this.HasEffect("starving"))
        //        {
        //            if (!this.HasEffect("deprived"))
        //            {
        //                this.AddEffect("deprived");
        //            }
        //            else
        //            {
        //                this.Effects.Find(e => e.GUID == "deprived").Value += 1;
        //            }
        //        }
        //    }
        //}
    }

    public void CalculateConsumption(Inventory storageInventory)
    {
        //if (this.People > 0)
        //{
        //    foreach (Good g in this.GoodsTemplate.Goods)
        //    {
        //        if (storageInventory.CanConsumeGood(g.ResourceItemGUID, g.RequiredAmount))
        //        {
        //            //Can consume good
        //            storageInventory.ConsumeGood(g.ResourceItemGUID, g.RequiredAmount);
        //            if (g.GoodType == GoodType.Necessity)
        //            {
        //                this.metNecessityRequirements = true;
        //            }
        //        }
        //        else
        //        {
        //            //Cannot consume good
        //            if (g.GoodType == GoodType.Necessity)
        //            {
        //                this.metNecessityRequirements = false;
        //            }
        //        }
        //    }
        //}
    }

    public void DetermineSeasonalEffects()
    {
        //if (this.People > 0)
        //{
        //    if (this.HasEffect("deprived"))
        //    {
        //        this.RemoveEffect("deprived");
        //        this.AddEffect("starving");
        //    }
        //    else if (this.HasEffect("sated"))
        //    {
        //        this.RemoveEffect("starving");
        //    }
        //}
    }

    public void ProcessTurnEffects()
    {
        //float workingHappiness = 10;
        //float workingNecessities  = 10;

        //if (this.People > 0)
        //{
        //    foreach (Effect e in this.Effects)
        //    {
        //        switch (e.EffectType)
        //        {
        //            case EffectType.Happiness: workingHappiness = e.ProcessValue(workingHappiness); break;
        //            case EffectType.Neccessities: workingNecessities  = e.ProcessValue(workingNecessities ); break;
        //            case EffectType.Population: /*Do nothing?*/ break;
        //        }
        //    }

        //    if (workingHappiness > 10) { workingHappiness = 10; }
        //    else if (workingHappiness < 0) { workingHappiness = 0; }

        //    if (workingNecessities  > 10) { workingNecessities  = 10; }
        //    else if (workingNecessities  < 0) { workingNecessities  = 0; }

        //    this.Happiness = workingHappiness;
        //    this.Necessities = workingNecessities ;
        //}
    }

    public void ProcessSeasonalEffects()
    {
        //if (this.People > 0)
        //{
        //    foreach (Effect e in this.Effects)
        //    {
        //        switch (e.GUID)
        //        {
        //            case "fulloflife":
        //                this.CalculatePopulationGrowth(e.Value);
        //                break;
        //            case "starving":
        //                this.CalculateStarvationDeath(e.Value);
        //                break;
        //            default:
        //                //Do nothing...
        //                break;
        //        }
        //    }
        //}
    }

    private void CalculatePopulationGrowth(float percentGain)
    {
        //float workingPopulation = this.People;
        //float amountToGain = (float)Math.Ceiling(this.People * percentGain);
        //workingPopulation += amountToGain;

        //this.People = (int)workingPopulation;
    }

    private void CalculateStarvationDeath(float percentLoss)
    {
        //float workingRemainingPopulation = this.People;
        //float amountToDie = (float)Math.Ceiling(this.People * percentLoss);
        //workingRemainingPopulation -= amountToDie;

        //this.People = (int)workingRemainingPopulation;
    }
}
