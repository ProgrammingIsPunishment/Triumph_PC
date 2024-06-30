using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

[Serializable]
public class CivilizationAI
{
    public Civilization CoupledCivilization;
    public int TerritorialExpansionDesire { get; set; }
    public int BuildUnitDesire { get; set; }
    public List<Interest> Interests { get; set; }
    public ObjectiveType ObjectiveType { get; set; }

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
        holdingClustersWithPresence = ownedHoldings.Select(oh => oh.HoldingValueSet.HoldingCluster).Distinct().ToList();

        List<Holding> unownedHoldingsInClustersWithPresence = new List<Holding>();
        foreach (Holding h in Oberkommando.SAVE.Holdings)
        {
            if (holdingClustersWithPresence.Any(hcwp => hcwp.Id == h.HoldingValueSet.HoldingCluster.Id))
            {
                if (h.TerrainType != TerrainType.Ocean && !h.IsOwner(this.CoupledCivilization))
                {
                    unownedHoldingsInClustersWithPresence.Add(h);
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

                int workingUnitProximity = 0;
                foreach (Unit u in Oberkommando.SAVE.Units)
                {
                    Holding holdingAtPosition = Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(u.XPosition,u.ZPosition);
                    int distance = Oberkommando.UTILITIES_SERVICE.DistanceBetweenHoldings(holdingAtPosition,i.Holding);

                    if (i.UnitProximityWeight == 0)
                    {
                        i.UnitProximityWeight = distance;
                    }
                    else if(i.UnitProximityWeight > distance)
                    {
                        i.UnitProximityWeight = distance;
                    }
                }
            }
        }
    }

    public void DetermineObjective()
    {
        this.ObjectiveType = ObjectiveType.TerritorialExpansion;
        //if (this.TerritorialExpansionDesire >= BuildUnitDesire) { this.ObjectiveType = ObjectiveType.TerritorialExpansion; }
        //else { this.ObjectiveType = ObjectiveType.BuildUnit; }
    }

    public void SendUnitOrders()
    {
        List<Unit> civilizationsUnits = Oberkommando.SAVE.Units.Where(u => u.Owner.GUID == this.CoupledCivilization.GUID).ToList();
        List<Interest> workingInterests = new List<Interest>();

        List<Interest> interestAlreadyBeingAddressed = new List<Interest>();
        List<Unit> unitsBeingSentOrders = new List<Unit>();

        switch (this.ObjectiveType)
        {
            case ObjectiveType.TerritorialExpansion:
                int highestValue = this.Interests.Max(i => i.TerritorialExpansionWeight);
                //workingInterests = this.Interests.Where(i => i.TerritorialExpansionWeight >= highestValue).ToList();
                workingInterests = this.Interests.Where(i => i.TerritorialExpansionWeight > 1).ToList();
                workingInterests = workingInterests.OrderBy(wi => wi.UnitProximityWeight).ToList();

                foreach (Interest i in workingInterests)
                {
                    Unit unitRecipient = null;
                    int tempLowestUnitDistance = 0;
                    foreach (Unit u in civilizationsUnits)
                    {
                        Holding holdingAtPosition = Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(u.XPosition, u.ZPosition);
                        Holding destinationHolding = Oberkommando.UTILITIES_SERVICE.GetHoldingByName(i.Holding.Name);
                        int distance = Oberkommando.UTILITIES_SERVICE.DistanceBetweenHoldings(holdingAtPosition, destinationHolding);

                        if (!unitsBeingSentOrders.Contains(u)) 
                        {
                            if (unitRecipient != null)
                            {
                                if (distance < tempLowestUnitDistance)
                                {
                                    unitRecipient = u;
                                    tempLowestUnitDistance = distance;
                                }
                            }
                            else
                            {
                                unitRecipient = u;
                                tempLowestUnitDistance = distance;
                                //if (distance <= i.UnitProximityWeight)
                                //{
                                //    unitRecipient = u;
                                //    tempLowestUnitDistance = distance;
                                //}
                            }
                        }
                    }

                    if (unitRecipient != null)
                    {
                        unitsBeingSentOrders.Add(unitRecipient);
                        Dispatch dispatch = new Dispatch(unitRecipient.Name, $"MOVE TO {i.Holding.Name}", unitRecipient.Owner, RecipientType.Unit);
                        Oberkommando.GAME_CONTROLLER.PendingDispatches.Add(dispatch);
                    }
                }
                break;
            default:
                //Do something eventually...
                break;
        }
    }
}
