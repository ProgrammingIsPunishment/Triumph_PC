using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimLeaderButton : MonoBehaviour
{
    public event EventHandler OnClaimLeaderButtonClick;

    public void OnClickEvent()
    {
        OnClaimLeaderButtonClick?.Invoke(this, EventArgs.Empty);
    }
}
