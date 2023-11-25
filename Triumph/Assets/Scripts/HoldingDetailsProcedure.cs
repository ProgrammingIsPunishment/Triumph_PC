using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum HoldingDetailsProcedureStep
{
    Show,
    Hide
}

public class HoldingDetailsProcedure
{
    public HoldingDetailsProcedureStep HoldingDetailsProcedureStep { get; set; }
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
            this.SelectedHolding.HoldingDisplayManager.ShowSelected(false);
            this.SelectedHolding = null;
            this.SelectedUnit = null;
        }
    }

    public void Handle(HoldingDetailsProcedureStep holdingDetailsProcedureStep)
    {
        switch (holdingDetailsProcedureStep)
        {
            case HoldingDetailsProcedureStep.Show:
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(true);
                Oberkommando.UI_CONTROLLER.holdingDetailsView.Refresh(this.SelectedHolding, this.SelectedUnit);
                Oberkommando.UI_CONTROLLER.holdingDetailsView.Default();
                Oberkommando.UI_CONTROLLER.holdingDetailsView.Show();
                Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails);
                break;
            case HoldingDetailsProcedureStep.Hide:
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(false);
                Oberkommando.UI_CONTROLLER.holdingDetailsView.Hide();
                break;
        }

        this.HoldingDetailsProcedureStep = holdingDetailsProcedureStep;
    }
}
