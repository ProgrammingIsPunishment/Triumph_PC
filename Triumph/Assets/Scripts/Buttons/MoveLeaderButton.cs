using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeaderButton : MonoBehaviour
{
    public event EventHandler OnMoveLeaderButtonClick;

    public void OnClickEvent()
    {
        Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.DisplayHoldingsWithinRange(true);
        //OnMoveLeaderButtonClick?.Invoke(this, EventArgs.Empty);
    }
}
