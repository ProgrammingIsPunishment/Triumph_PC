using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDispatchButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Dispatch dispatch = Oberkommando.UI_CONTROLLER.DispatchesManager.GetDispatch();
        Oberkommando.UI_CONTROLLER.DispatchesManager.Show(false);
        Oberkommando.UI_CONTROLLER.DispatchesManager.Default();
        Debug.Log(dispatch.RecipientName + " : " + dispatch.Message);
    }
}
