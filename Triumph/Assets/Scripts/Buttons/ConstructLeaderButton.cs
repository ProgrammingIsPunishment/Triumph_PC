using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        //Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.ConstructLeader);
        //Holding tempHolding = Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.SelectedHolding;
        //Unit tempUnit = Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.SelectedUnit;
        ImprovementsView tempImporvementsView = Oberkommando.UI_CONTROLLER.holdingView.improvementsTab;
        Oberkommando.UI_CONTROLLER.holdingView.SwitchTab(HoldingDetailsTabType.Improvements);
        //Oberkommando.UI_CONTROLLER.ConstructLeaderProcedure.AssignFields(tempHolding, tempUnit, tempImporvementsView);
        //Oberkommando.UI_CONTROLLER.ConstructLeaderProcedure.Handle(ConstructLeaderProcedureStep.ShowAvailableSlots);
    }
}
