using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController
{
    public void EndTurn()
    {
        //Oberkommando.UI_CONTROLLER.NewUIState(UIState.Disable, null);
        Oberkommando.SAVE.Turn++;
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

    public void StartTurn(Civilization currentCivilization)
    {
        //For player character only as NPCs don't need to actually "see" anything
        Oberkommando.UI_CONTROLLER.MapRefresh(currentCivilization);
        this.UnitEvents(currentCivilization.Units);
        //NEED TO DO...do passover of holdings that are not currently fully visible

        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.ShowExplorableHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.RefreshToDefault();
    }
}
