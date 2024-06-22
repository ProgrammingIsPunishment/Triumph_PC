using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CivilizationAI
{
    public Civilization CoupledCivilization;
    public int TerritorialExpansionDesire { get; set; }
    public int BuildUnitDesire { get; set; }
    public List<Interest> Interests { get; set; }

    public CivilizationAI(Civilization civilization)
    {
        this.CoupledCivilization = civilization;
        this.TerritorialExpansionDesire = 1;
        this.BuildUnitDesire = 1;

        this.Interests = new List<Interest>();
    }

    public void CalculateDesires()
    {
        this.TerritorialExpansionDesire = 1;
        this.BuildUnitDesire = 1;

        List<Unit> ownedUnits = Oberkommando.SAVE.Units.Where(u => u.Owner.GUID == this.CoupledCivilization.GUID && u.UnitTemplate.GUID != "leadertemplate").ToList();
        List<Holding> ownedHoldings = new List<Holding>();
        foreach (Holding h in Oberkommando.SAVE.Holdings)
        {
            if (h.Owner != null)
            {
                if (h.Owner.GUID == this.CoupledCivilization.GUID) { ownedHoldings.Add(h); }
            }
        }

        int income = ownedHoldings.Count() - ownedUnits.Count();

        if (income < 5) { this.TerritorialExpansionDesire++; }
        if (this.CoupledCivilization.Coins >= 10) { this.BuildUnitDesire++; }
    }

    public void CalculateInterests()
    {
        //List<HoldingCluster> holdingClustersWithPresence = Oberkommando.VALUELIBRARY.HoldingValueSets.
        foreach (Interest i in this.Interests)
        {
            if (i.Holding != null)
            {
                
            }
        }
    }
}
