using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController
{
    private bool seasonChangedFlag = false;

    public void EndTurn()
    {
        //Oberkommando.UI_CONTROLLER.NewUIState(UIState.Disable, null);
        this.CheckSeasonForChange();
        Oberkommando.SAVE.Turn++;
        //this.seasonChangedFlag = false;
        Debug.Log(Oberkommando.SAVE.Turn.ToString());
        //this.RefreshActionPoints(Oberkommando.SAVE.AllCivilizations[0]);
    }

    public void UnitEvents(List<Unit> units)
    {
        foreach (Unit u in units)
        {
            u.CalculateAttrition();
            u.Recover();
        }
    }

    public void PopulationEvents(List<Holding> allHoldings)
    {
        //NEED TO DO...
        foreach (Holding h in allHoldings)
        {
            h.Population.ProcessConsumption(h.StorageInventory);
            //h.PassEffectFromHolding();
            h.Population.DetermineTurnEffects();
            h.Population.ProcessTurnEffects();

            if (this.seasonChangedFlag)
            {
                h.Population.DetermineSeasonalEffects();
                h.Population.ProcessSeasonalEffects();
                //h.CalculateSeasonalGrowth();
            }
        }
    }

    public void HoldingEvents(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            h.PassEffectFromHolding();

            if (this.seasonChangedFlag)
            {
                List<string> growthSeasons = new List<string>() { "Spring", "Summer", "Autumn" };
                if (growthSeasons.Contains(Oberkommando.SAVE.Season.Name))
                {
                    h.CalculateSeasonalGrowth();
                }
            }
        }
    }

    public void CheckSeasonForChange()
    {
        Oberkommando.SAVE.Season.DaysLeft--;
        if (Oberkommando.SAVE.Season.DaysLeft < 0)
        {
            this.seasonChangedFlag = true;
            int nextSeasonOrder = Oberkommando.SAVE.Season.Order + 1;
            if (nextSeasonOrder > 4) { nextSeasonOrder = 1; }
            Oberkommando.SAVE.Season.ResetDaysLeft();
            Oberkommando.SAVE.Season = Oberkommando.SAVE.AllSeasons.Find(s => s.Order == nextSeasonOrder);
        }
    }

    public void StartTurn(Civilization currentCivilization)
    {
        //For player character only as NPCs don't need to actually "see" anything
        this.HoldingEvents(Oberkommando.SAVE.AllHoldings);
        this.UnitEvents(currentCivilization.Units);
        this.PopulationEvents(Oberkommando.SAVE.AllHoldings);
        this.seasonChangedFlag = false;
        currentCivilization.ReplenishPoliticalPower();
        Oberkommando.UI_CONTROLLER.MapRefresh(currentCivilization);
        Oberkommando.UI_CONTROLLER.seasonsView.Refresh(Oberkommando.SAVE.Season);
        Oberkommando.UI_CONTROLLER.politicalPowerView.Refresh(currentCivilization.PoliticalPower);
        //NEED TO DO...do passover of holdings that are not currently fully visible

        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.ShowExplorableHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.RefreshToDefault();
    }
}
