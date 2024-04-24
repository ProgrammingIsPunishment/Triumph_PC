using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimLeaderButton : MonoBehaviour
{
    public event EventHandler OnClaimLeaderButtonClick;

    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.ResetViews();
        Oberkommando.SELECTED_HOLDING.ClaimTerritory(Oberkommando.SAVE.AllCivilizations[0]);
        Oberkommando.SAVE.AllCivilizations[0].UsePoliticalPower(1);

        Oberkommando.UI_CONTROLLER.politicalPowerView.Refresh(Oberkommando.SAVE.AllCivilizations[0].PoliticalPower);
        Oberkommando.UI_CONTROLLER.holdingView.Refresh(Oberkommando.SELECTED_HOLDING, Oberkommando.SELECTED_UNIT);

        Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.ShowSelected(true);
        Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.ShowBorder(true);
    }
}
