using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        //Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderGather_SelectResourceItem);
        Oberkommando.UI_CONTROLLER.holdingView.naturalResourcesTab.ShowGatherableResources(true);
        Oberkommando.UI_CONTROLLER.holdingView.SwitchTab(HoldingDetailsTabType.NaturalResources);
    }
}
