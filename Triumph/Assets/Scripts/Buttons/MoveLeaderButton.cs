using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeaderButton : MonoBehaviour
{
    public event EventHandler OnMoveLeaderButtonClick;

    public void OnClickEvent()
    {
        OnMoveLeaderButtonClick?.Invoke(this, EventArgs.Empty);
    }
}
