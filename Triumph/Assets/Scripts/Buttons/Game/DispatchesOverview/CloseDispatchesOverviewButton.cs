using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDispatchesOverviewButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.OutgoingDispatchesManager.Show(false);
    }
}
