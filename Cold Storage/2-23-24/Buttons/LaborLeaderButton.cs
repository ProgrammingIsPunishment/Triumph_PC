using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaborLeaderButton : MonoBehaviour
{
    public void ClickEvent()
    {
        //Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderLabor_SelectImprovement);
        Oberkommando.UI_CONTROLLER.ResetViews();
        Oberkommando.UI_CONTROLLER.holdingView.SwitchTab(HoldingDetailsTabType.Improvements);
        Oberkommando.UI_CONTROLLER.holdingView.improvementsTab.ShowNeededLabor(true);
    }
}
