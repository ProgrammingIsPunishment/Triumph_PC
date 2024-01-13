using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIData
{
    public Holding Holding { get; set; } = null;
    public Unit Unit { get; set; } = null;

    public UIData None()
    {
        return this;
    }

    public UIData HoldingDetails(Holding holding, Unit unit)
    {
        this.Holding = holding;
        this.Unit = unit;

        return this;
    }
}
