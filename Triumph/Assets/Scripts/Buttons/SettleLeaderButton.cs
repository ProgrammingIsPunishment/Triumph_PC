using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettleLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderSettle_End);
    }
}
