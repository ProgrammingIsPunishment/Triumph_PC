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
        List<Holding> ownedHoldings = new List<Holding>();
        foreach (Holding h in Oberkommando.SAVE.Holdings) { if (h.Owner != null) { if (h.Owner.GUID == this.CoupledCivilization.GUID) { ownedHoldings.Add(h); } } }

        #region Clusters With Presence _______________________________________________________________________________________________________
        List<HoldingCluster> holdingClustersWithPresence = new List<HoldingCluster>();
        foreach (HoldingValueSet hvs in Oberkommando.VALUELIBRARY.HoldingValueSets)
        {
            if (ownedHoldings.Any(oh => oh.GUID == hvs.HoldingGUD) == true) { holdingClustersWithPresence.Add(hvs.HoldingCluster); }
        }
        holdingClustersWithPresence = holdingClustersWithPresence.Distinct().ToList();

        List<Holding> unownedHoldingsInClustersWithPresence = new List<Holding>();
        foreach (HoldingValueSet hvs in Oberkommando.VALUELIBRARY.HoldingValueSets)
        {
            //Is holding unowned by civilization
            if (holdingClustersWithPresence.Any(hcwp => hcwp.Id == hvs.HoldingCluster.Id))
            {
                Holding holding = Oberkommando.SAVE.Holdings.Find(h => h.GUID == hvs.HoldingGUD);
                if (holding.TerrainType != TerrainType.Ocean && !holding.IsOwner(this.CoupledCivilization))
                {
                    unownedHoldingsInClustersWithPresence.Add(holding);
                }
            }
        }
        #endregion

        #region Adjacent Holdings _______________________________________________________________________________________________________
        List<Holding> adjacentHoldings = new List<Holding>();
        foreach (Holding h in ownedHoldings)
        {
            List<Holding> workingHoldings = h.AdjacentHoldings;

            foreach (Holding ah in workingHoldings)
            {
                if (ah.TerrainType != TerrainType.Ocean && !ah.IsOwner(this.CoupledCivilization))
                {
                    adjacentHoldings.Add(ah);
                }
            }
        }
        #endregion

        foreach (Interest i in this.Interests)
        {
            i.Reset();
            
            if (i.Holding != null)
            {
                if (unownedHoldingsInClustersWithPresence.Contains(i.Holding)) { i.TerritorialExpansionWeight+=2; }
                if (adjacentHoldings.Contains(i.Holding)) { i.TerritorialExpansionWeight++; }
                i.TerritorialExpansionWeight++;
            }
        }
    }
}
