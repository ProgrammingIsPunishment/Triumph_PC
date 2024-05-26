using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DispatchesOverviewButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        List<Dispatch> dispatches = Oberkommando.SAVE.Dispatches.Where(d=>d.Owner == Oberkommando.PLAYER).ToList();
        dispatches.AddRange(Oberkommando.GAME_CONTROLLER.PendingDispatches);
        Oberkommando.UI_CONTROLLER.OutgoingDispatchesManager.Refresh(dispatches);
        Oberkommando.UI_CONTROLLER.OutgoingDispatchesManager.Show(true);
    }
}
