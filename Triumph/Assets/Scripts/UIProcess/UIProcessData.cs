using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProcessData
{
    public Holding Holding = null;
    
    public HoldingDetailsProcessState? HoldingDetailsProcessState = null;

    public UIProcessData(Holding holding, HoldingDetailsProcessState holdingDetailsProcessState)
    {
        this.Holding = holding;
        this.HoldingDetailsProcessState = holdingDetailsProcessState;
    }
}
