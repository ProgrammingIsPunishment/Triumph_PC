using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDispatchesButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Oberkommando.UI_CONTROLLER.DispatchesManager.Show(false);
    }
}
