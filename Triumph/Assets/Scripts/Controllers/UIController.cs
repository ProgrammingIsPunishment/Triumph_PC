using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public HoldingDetailsManager holdingdetailsManager;

    public void Open(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.HoldingDetails: break;
            default: break;
        }
    }
}
