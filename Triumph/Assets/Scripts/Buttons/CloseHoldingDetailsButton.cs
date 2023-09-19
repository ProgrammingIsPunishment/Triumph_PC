using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHoldingDetailsButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Oberkommando.UI_CONTROLLER.Hide(UIType.HoldingDetails);
    }
}
