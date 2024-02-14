using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHoldingViewButton : MonoBehaviour
{
    public event EventHandler OnHoldingViewCloseEvent;

    public void OnClickEvent()
    {
        OnHoldingViewCloseEvent?.Invoke(this, EventArgs.Empty);
    }
}
