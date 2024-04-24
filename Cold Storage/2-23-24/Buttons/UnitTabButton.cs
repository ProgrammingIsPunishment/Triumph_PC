using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTabButton : MonoBehaviour
{
    [SerializeField] private UnitTabType unitTabType;

    public void ClickEvent()
    {
        Oberkommando.UI_CONTROLLER.holdingView.unitView.SwitchTab(this.unitTabType);
    }
}
