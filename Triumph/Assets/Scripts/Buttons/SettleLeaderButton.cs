using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettleLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        List<Pop> tempPeopleToSettle = Oberkommando.SELECTED_UNIT.Population.Pops;
        Oberkommando.SELECTED_UNIT.Settle();
        Oberkommando.SELECTED_HOLDING.Population.Settle(tempPeopleToSettle);
        Oberkommando.SELECTED_HOLDING.PassEffectFromHolding();
        Oberkommando.SELECTED_HOLDING.Population.DetermineTurnEffects();
        Oberkommando.UI_CONTROLLER.holdingView.Refresh(Oberkommando.SELECTED_HOLDING, Oberkommando.SELECTED_UNIT);
        Oberkommando.SELECTED_HOLDING.HoldingDisplayManager.ShowSelected(true);
    }
}
