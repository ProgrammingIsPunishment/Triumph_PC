using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderGather_SelectResourceItem);
    }
}
