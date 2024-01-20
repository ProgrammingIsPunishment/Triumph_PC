using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderBuild_SelectLot);
    }
}
