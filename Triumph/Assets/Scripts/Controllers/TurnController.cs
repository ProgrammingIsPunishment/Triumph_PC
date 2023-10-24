using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController
{
    public void EndTurn()
    {
        Oberkommando.SAVE.Turn++;
        Debug.Log(Oberkommando.SAVE.Turn.ToString());
        this.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }

    public void StartTurn(Civilization currentCivilization)
    {
        //For player character only as NPCs don't need to actually "see" anything
        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(currentCivilization);
        //Oberkommando.UI_CONTROLLER.ShowExplorableHoldings(currentCivilization);
    }
}
