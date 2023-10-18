using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProcessData
{
    public Holding Holding = null;

    public Holding Holding2 = null;

    public HoldingDetailsProcessState? HoldingDetailsProcessState = null;

    public MoveLeaderUnitProcessState? MoveLeaderUnitProcessState = null;

    public UIProcessData(Holding holding, HoldingDetailsProcessState holdingDetailsProcessState)
    {
        this.Holding = holding;
        this.HoldingDetailsProcessState = holdingDetailsProcessState;
    }

    public UIProcessData(Holding currentHolding, Holding destinationHolding, MoveLeaderUnitProcessState moveLeaderUnitProcessState)
    {
        this.Holding = currentHolding;
        this.Holding2 = destinationHolding;
        this.MoveLeaderUnitProcessState = moveLeaderUnitProcessState;
    }
}
