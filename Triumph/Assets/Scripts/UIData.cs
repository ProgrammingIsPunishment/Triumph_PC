using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIData
{
    //Holding Details
    public HoldingDetailsViewState HoldingDetailsViewState { get; set; } = HoldingDetailsViewState.Show;
    public Holding HoldingDetails_Holding { get; set; }
    public Unit HoldingDetails_Unit { get; set; }
    public Holding HoldingDetails_DestinationHolding { get; set; }

    public void AssignHoldingDetailsData(Holding holding, Unit unit)
    {
        this.HoldingDetailsViewState = HoldingDetailsViewState.Show;
        this.HoldingDetails_Holding = holding;
        this.HoldingDetails_Unit = unit;
    }
}
