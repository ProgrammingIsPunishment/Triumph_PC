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
                Oberkommando.UI_CONTROLLER.holdingView.Refresh(this.SelectedHolding, this.SelectedUnit);
                //Oberkommando.UI_CONTROLLER.holdingView.Default();
                Oberkommando.UI_CONTROLLER.holdingView.Show();
                Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.HoldingDetails);
                break;
            case HoldingDetailsProcedureStep.Hide:
                this.SelectedHolding.HoldingDisplayManager.ShowSelected(false);
                Oberkommando.UI_CONTROLLER.holdingView.Hide();
                break;
        }

        this.HoldingDetailsProcedureStep = holdingDetailsProcedureStep;
    }
}
