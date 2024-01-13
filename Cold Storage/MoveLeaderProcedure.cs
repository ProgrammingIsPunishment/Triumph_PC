using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum MoveLeaderProcedureStep
{
    ShowAvailableHoldings,
    Move
}

public class MoveLeaderProcedure
{
    public MoveLeaderProcedureStep MoveLeaderProcedureStep { get; set; }
    public Holding SelectedHolding { get; private set; }
    public Unit SelectedUnit { get; private set; }
    public Holding DestinationHolding { get; set; } 

    public void AssignFields(Holding holding, Unit unit)
    {
        this.SelectedHolding = holding;
        this.SelectedUnit = unit;
    }

    public void Reset()
    {
        if (this.SelectedHolding != null)
        {
            this.ShowHoldingsWithinRange(false, this.SelectedHolding);
            this.SelectedHolding = null;
            this.SelectedUnit = null;
        }
    }

    public void Handle(MoveLeaderProcedureStep moveLeaderProcedureStep)
    {
        switch (moveLeaderProcedureStep)
        {
            case MoveLeaderProcedureStep.ShowAvailableHoldings:
                this.ShowHoldingsWithinRange(true,this.SelectedHolding);
                break;
            case MoveLeaderProcedureStep.Move:
                if (this.SelectedHolding.AdjacentHoldings.Contains(this.DestinationHolding))
                {
                    this.SelectedUnit.Move(this.DestinationHolding.XPosition, this.DestinationHolding.ZPosition);
                    this.ShowHoldingsWithinRange(false, this.SelectedHolding);
                    this.DestinationHolding.UpdateVisibility(Oberkommando.SAVE.AllCivilizations[0]);

                    //Bring up holding details
                    //Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.Reset();
                    //Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.AssignFields(this.DestinationHolding, this.SelectedUnit);
                    //Oberkommando.UI_CONTROLLER.HoldingDetailsProcedure.Handle(HoldingDetailsProcedureStep.Show);
                    Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.HoldingDetails);

                    this.Reset();
                }
                break;
        }

        this.MoveLeaderProcedureStep = moveLeaderProcedureStep;
    }

    public void ShowHoldingsWithinRange(bool isBeingShown, Holding holding)
    {
        if (isBeingShown)
        {
            foreach (Holding h in holding.AdjacentHoldings)
            {
                h.HoldingDisplayManager.ShowSelectable(true);
            }
        }
        else
        {
            foreach (Holding h in holding.AdjacentHoldings)
            {
                h.HoldingDisplayManager.ShowSelectable(false);
            }
        }
    }
}
