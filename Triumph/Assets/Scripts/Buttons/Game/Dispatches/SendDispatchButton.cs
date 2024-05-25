using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendDispatchButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Dispatch dispatch = Oberkommando.UI_CONTROLLER.DispatchesManager.GetDispatch();
        Oberkommando.UI_CONTROLLER.DispatchesManager.Show(false);
        Oberkommando.UI_CONTROLLER.DispatchesManager.Default();
        Oberkommando.GAME_CONTROLLER.PendingDispatches.Add(dispatch);
        //Debug.Log(dispatch.RecipientName + " : " + dispatch.Message);
        //Oberkommando.PLAYER.Dispatches.Add(dispatch);
        //Debug.Log(Oberkommando.SAVE.Dispatches);
    }

    public void Enabled(bool isEnabled)
    {
        this.GetComponent<Button>().interactable = isEnabled;
    }
}
