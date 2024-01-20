using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaborLeaderButton : MonoBehaviour
{
    public void ClickEvent()
    {
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderLabor_SelectImprovement);
    }
}
