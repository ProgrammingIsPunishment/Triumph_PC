using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController
{
    public void EndTurn()
    {
        //Oberkommando.UI_CONTROLLER.NewUIState(UIState.Disable, null);
        this.PopulationEvents(Oberkommando.SAVE.AllHoldings);
        Oberkommando.SAVE.Turn++;
        this.ChangeSeason();
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
            h.Population.CalculateConsumption(h.StorageInventory);
            h.PassEffectFromHolding();
            h.Population.DetermineEffects();
            h.Population.ProcessEffects();
        }
    }

    public void ChangeSeason()
    {
        Oberkommando.SAVE.Season.DaysLeft--;
        if (Oberkommando.SAVE.Season.DaysLeft <= 0)
        {
            int nextSeasonOrder = Oberkommando.SAVE.Season.Order + 1;
            if (nextSeasonOrder > 4) { nextSeasonOrder = 1; }
            Oberkommando.SAVE.Season.ResetDaysLeft();
            Oberkommando.SAVE.Season = Oberkommando.SAVE.AllSeasons.Find(s => s.Order == nextSeasonOrder);
        }
    }

    public void StartTurn(Civilization currentCivilization)
    {
        //For player character only as NPCs don't need to actually "see" anything
        Oberkommando.UI_CONTROLLER.MapRefresh(currentCivilization);
        Oberkommando.UI_CONTROLLER.seasonsView.Refresh(Oberkommando.SAVE.Season);
        this.UnitEvents(currentCivilization.Units);
        //NEED TO DO...do passover of holdings that are not currently fully visible

        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.ShowExplorableHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.RefreshToDefault();
    }
}
