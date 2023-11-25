using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GatherLeaderProcedureStep
{
    ShowAvailableResources,
    Gather
}

public class GatherLeaderProcedure
{
    public GatherLeaderProcedureStep GatherLeaderProcedureStep { get; set; }
    public Holding SelectedHolding { get; private set; }
    public Unit SelectedUnit { get; private set; }
    public ResourceItem SelectedResourceItem { get; set; }

    public void AssignFields(Holding holding, Unit unit)
    {
        this.SelectedHolding = holding;
        this.SelectedUnit = unit;
    }

    public void Reset()
    {
        if (this.SelectedHolding != null)
        {
            this.SelectedHolding = null;
            this.SelectedUnit = null;
            this.SelectedResourceItem = null;
        }
    }

    public void Handle(GatherLeaderProcedureStep gatherLeaderProcedureStep)
    {
        switch (gatherLeaderProcedureStep)
        {
            case GatherLeaderProcedureStep.ShowAvailableResources:
                this.ShowAvailableResources(this.SelectedHolding.NaturalResourcesInventory);
                break;
            case GatherLeaderProcedureStep.Gather:
                this.SelectedUnit.Gather(this.SelectedResourceItem);

                Oberkommando.UI_CONTROLLER.holdingDetailsView.Refresh(this.SelectedHolding,this.SelectedUnit);
                //Oberkommando.UI_CONTROLLER.holdingDetailsView.naturalResourcesTab.Refresh(this.SelectedHolding.NaturalResourcesInventory);
                //Oberkommando.UI_CONTROLLER.holdingDetailsView.unitSupplyTab.Refresh(this.SelectedUnit.SupplyInventory);

                Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails);

                this.Reset();
                break;
        }

        this.GatherLeaderProcedureStep = gatherLeaderProcedureStep;
    }

    private void ShowAvailableResources(Inventory inventory)
    {
        //Eventually will need to disable and enable only items that the unit can gather
        //For right now, everything is enabled for the sake of testing
        foreach (ResourceItem ri in inventory.ResourceItems)
        {
            ri.InventorySlotView.Disable();
        }

        foreach (ResourceItem ri in inventory.ResourceItems)
        {
            ri.InventorySlotView.Enable();
        }
    }
}
