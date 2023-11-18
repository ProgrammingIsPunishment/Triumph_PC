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
        this.RefreshActionPoints(Oberkommando.SAVE.AllCivilizations[0]);
        this.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }

    public void RefreshActionPoints(Civilization civilization)
    {
        foreach (Unit u in civilization.Units)
        {
            //u.RefreshActionPoints();
        }
    }

    public void StartTurn(Civilization currentCivilization)
    {
        //For player character only as NPCs don't need to actually "see" anything
        Oberkommando.UI_CONTROLLER.MapRefresh(Oberkommando.SAVE.AllCivilizations[0]);
        //NEED TO DO...do passover of holdings that are not currently fully visible

        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.ShowExplorableHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.RefreshToDefault();
    }
}
