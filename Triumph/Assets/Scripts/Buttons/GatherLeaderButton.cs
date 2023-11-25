using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherLeaderButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.NewUIState(UIState.GatherLeader);
        Holding tempHolding = Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.SelectedHolding;
        Unit tempUnit = Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.SelectedUnit;
        Oberkommando.UI_CONTROLLER.holdingDetailsView.SwitchTab(HoldingDetailsTabType.NaturalResources);
        Oberkommando.UI_CONTROLLER.GatherLeaderProcedure.AssignFields(tempHolding, tempUnit);
        Oberkommando.UI_CONTROLLER.GatherLeaderProcedure.Handle(GatherLeaderProcedureStep.ShowAvailableResources);
        //Holding selectedHolding = Oberkommando.UI_CONTROLLER.SelectedHoldings[0];
        //selectedHolding.Unit.Gather(selectedHolding.NaturalResourcesInventory, gatherType);
    }
}
