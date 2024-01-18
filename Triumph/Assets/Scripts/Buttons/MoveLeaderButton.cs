using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
       Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderMove_SelectHolding);
    }
}
