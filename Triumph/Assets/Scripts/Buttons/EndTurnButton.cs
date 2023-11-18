using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails);
        Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.Handle(HoldingDetailsProcedureStep.Hide);
        Oberkommando.UI_CONTROLLER.MoveLeaderProcedure.Reset();
        Oberkommando.TURN_CONTROLLER.EndTurn();
    }
}
