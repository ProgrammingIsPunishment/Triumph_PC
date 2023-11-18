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
                break;
        }

        this.GatherLeaderProcedureStep = gatherLeaderProcedureStep;
    }

    private void ShowAvailableResources(Inventory inventory)
    {
        foreach (ResourceItem ri in inventory.ResourceItems)
        {
            //ri.
        }
    }
}
