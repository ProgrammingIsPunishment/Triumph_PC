using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Oberkommando.TURN_CONTROLLER.EndTurn();
    }
}
