using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHoldingDetailsButton : MonoBehaviour
{
    public void ClickEvent()
    {
        //Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.Handle(HoldingDetailsProcedureStep.Hide);
        //Oberkommando.UI_CONTROLLER.MoveLeaderProcedure.Reset();
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.HoldingDetails_End);
    }
}
