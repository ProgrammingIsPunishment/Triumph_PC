using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AICivilizationProfile
{
    public int TerritorialExpansionDesire { get; set; }
    public List<IAIGoal> AIGoals { get; set; } = new List<IAIGoal>();

    public AICivilizationProfile()
    {
        this.TerritorialExpansionDesire = 1;
    }

    public void NewGoal(Civilization civilization)
    {
        if (this.TerritorialExpansionDesire >= 1)
        {
            //Civilization wants to expand its territory

            List<Holding> ownedHoldings = new List<Holding>();
            foreach (Holding h in Oberkommando.SAVE.Holdings) { if (h.Owner != null) { if (h.Owner.GUID == civilization.GUID) { ownedHoldings.Add(h); } } }

            List<HoldingClusterSet> holdingClusterSets= Oberkommando.VALUELIBRARY.HoldingClusterSetsTerritoriesAreIn(ownedHoldings);

            List<Holding> holdingsToCapture = null;
            int tempCurrentHighest = 0;
            foreach (HoldingClusterSet hcs in holdingClusterSets)
            {
                List<Holding> workingHoldingsToCapture = new List<Holding>();
                foreach (Holding h in hcs.Holdings) 
                { 
                    if (h.Owner != null) 
                    {
                        if (h.Owner.GUID != civilization.GUID && h.TerrainType != TerrainType.Ocean) { workingHoldingsToCapture.Add(h); }
                    }
                    else
                    {
                        if (h.TerrainType != TerrainType.Ocean) { workingHoldingsToCapture.Add(h); }
                    }
                }
                int count = workingHoldingsToCapture.Count();

                if (tempCurrentHighest < count)
                {
                    holdingsToCapture = workingHoldingsToCapture;
                }
            }

            this.AIGoals.Add(new AITerritorialExpansionGoal(holdingsToCapture));
        }
    }
}
