using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeleteDispatchButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        //Delete
        Dispatch dispatchToDelete = Oberkommando.UI_CONTROLLER.OutgoingDispatchesManager.SelectedDispatch;
        Oberkommando.GAME_CONTROLLER.PendingDispatches.Remove(dispatchToDelete);

        //Refresh
        List<Dispatch> dispatches = Oberkommando.SAVE.Dispatches.Where(d => d.Owner == Oberkommando.PLAYER).ToList();
        dispatches.AddRange(Oberkommando.GAME_CONTROLLER.PendingDispatches);
        Oberkommando.UI_CONTROLLER.OutgoingDispatchesManager.Refresh(dispatches);
        Oberkommando.UI_CONTROLLER.OutgoingDispatchesManager.Show(true);
    }

    public void Show(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }
}
