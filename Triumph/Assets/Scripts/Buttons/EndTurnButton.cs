using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.EndTurn);
        Oberkommando.TURN_CONTROLLER.EndTurn();
        Oberkommando.TURN_CONTROLLER.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }
}
