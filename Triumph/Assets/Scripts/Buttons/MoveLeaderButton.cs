using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.MoveLeaderProcess(MoveLeaderSubState.ShowSelectableHoldings,null);
        //UIProcessData uiProcessState = new UIProcessData(Oberkommando.UI_CONTROLLER.SelectedHoldings[0], null, MoveLeaderUnitProcessState.ShowSelectable);
        //Oberkommando.UI_CONTROLLER.NewUIState(UIState.MoveLeader, uiProcessState);
    }
}
