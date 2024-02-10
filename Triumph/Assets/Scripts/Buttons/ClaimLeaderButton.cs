using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderClaim_End);
    }
}
