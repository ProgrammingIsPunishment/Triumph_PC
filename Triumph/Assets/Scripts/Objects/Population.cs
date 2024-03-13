using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        foreach (Pop p in this.Pops)
        {
            if (p.Happiness >= 7)
            {
                p.AddEffect("fulloflife");
            }
            else
            {
                p.RemoveEffect("fulloflife");
            }

            if (p.metNecessityRequirements)
            {
                p.AddEffect("sated");
            }
            else
            {
                p.RemoveEffect("sated");

                if (!p.HasEffect("starving"))
                {
                    if (!p.HasEffect("deprived"))
                    {
                        p.AddEffect("deprived");
                    }
                    else
                    {
                        p.Effects.Find(e => e.GUID == "deprived").Value += 1;
                    }
                }
            }
        }
    }

    public void ProcessConsumption(Inventory storageInventory)
    {
        if (this.Pops.Count > 0)
        {
            foreach (Pop p in this.Pops)
            {
                p.Consume(storageInventory);
            }
        }
    }

    public void DetermineSeasonalEffects()
    {
        foreach (Pop p in this.Pops)
        {
            if (p.HasEffect("deprived"))
            {
                p.RemoveEffect("deprived");
                p.AddEffect("starving");
            }
            else if (p.HasEffect("sated"))
            {
                p.RemoveEffect("starving");
            }
        }
    }

    public void ProcessTurnEffects()
    {
        float workingHappiness = 10;
        float workingNecessities = 10;

        foreach (Pop p in this.Pops)
        {
            foreach (Effect e in p.Effects)
            {
                switch (e.EffectType)
                {
                    case EffectType.Happiness: workingHappiness = e.ProcessValue(workingHappiness); break;
                    case EffectType.Neccessities: workingNecessities = e.ProcessValue(workingNecessities); break;
                    case EffectType.Population: /*Do nothing?*/ break;
                }
            }

            if (workingHappiness > 10) { workingHappiness = 10; }
            else if (workingHappiness < 0) { workingHappiness = 0; }

            if (workingNecessities > 10) { workingNecessities = 10; }
            else if (workingNecessities < 0) { workingNecessities = 0; }

            p.Happiness = workingHappiness;
            p.Necessities = workingNecessities;
        }
    }

    public void ProcessSeasonalEffects()
    {
        this.CalculatePopulationGrowth(0);
        this.CalculateStarvationDeath(0);

        //foreach (Pop p in this.Pops)
        //{
        //    foreach (Effect e in p.Effects)
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

        //int maleCount = this.Pops.Count(p => p.IsMale == true && p.Level == 5);
        //int femaleCount = this.Pops.Count(p => p.IsMale != true && p.Level == 5);

        //if (maleCount != 0 & femaleCount != 0)
        //{
        //    //Calculate the pairs
        //    int workingPairs = maleCount;
        //    if (workingPairs > femaleCount) { workingPairs = femaleCount; }
        //}

        List<Pop> eligibleMalePops = this.Pops.Where(p => p.IsMale == true && p.Level == 0).ToList();
        List<Pop> eligibleFemalePops = this.Pops.Where(p => p.IsMale == false && p.Level == 0).ToList();

        if (eligibleMalePops.Count() >= 1 && eligibleFemalePops.Count() >= 1)
        {
            List<Pop> listOne = null;
            List<Pop> listTwo = null;

            if (Tools.IsFirstNumberGreater(eligibleMalePops.Count(), eligibleFemalePops.Count()))
            {
                listOne = eligibleMalePops;
                listTwo = eligibleFemalePops;
            }
            else
            {
                listOne = eligibleFemalePops;
                listTwo = eligibleMalePops;
            }

            List<Pop> workingNewPops = new List<Pop>();
            for (int i = 0; i < listOne.Count; i++)
            {
                //Has to be a male and a female
                if (i < listTwo.Count)
                {
                    Debug.Log("New Pop!");
                    bool tempIsMale = Tools.GetRandomBoolean();
                    workingNewPops.Add(new Pop(listOne[i].GoodsTemplate, tempIsMale));

                    listOne[i].Level = 0;
                    listTwo[i].Level = 0;
                }
            }

            this.Pops.AddRange(workingNewPops);
            //int eligibleCount = 0;
            //bool usingMaleCount = true;
            //if (eligibleMalePops.Count() >= eligibleFemalePops.Count()) { eligibleCount = eligibleMalePops.Count(); usingMaleCount = true; }
            //else { eligibleCount = eligibleFemalePops.Count(); usingMaleCount = false; }

            //List<Pop> tempPops = null;
            //if (usingMaleCount) { tempPops = eligibleMalePops; }
            //else { tempPops = eligibleFemalePops; }

            //for (int i = 0; i < tempPops.Count; i++)
            //{

            //}
        }

        //Level up each of the pops
        //foreach (Pop p in this.Pops)
        //{
        //    p.LevelUp();
        //}
    }

    private void CalculateStarvationDeath(float percentLoss)
    {
        //float workingRemainingPopulation = this.People;
        //float amountToDie = (float)Math.Ceiling(this.People * percentLoss);
        //workingRemainingPopulation -= amountToDie;

        //this.People = (int)workingRemainingPopulation;
    }
}
