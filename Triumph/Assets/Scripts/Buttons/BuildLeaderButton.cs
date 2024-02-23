using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLeaderButton : MonoBehaviour
{
    public event EventHandler OnBuildLeaderButtonClick;

    public void OnClickEvent()
    {
        //OnBuildLeaderButtonClick?.Invoke(this, EventArgs.Empty);

        Oberkommando.UI_CONTROLLER.holdingView.SwitchTab(HoldingDetailsTabType.Improvements);
        Oberkommando.UI_CONTROLLER.holdingView.improvementsTab.ShowImprovableLots(true);
    }
}
