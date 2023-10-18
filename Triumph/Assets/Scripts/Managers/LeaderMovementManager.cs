using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderMovementManager
{

    public void MoveLeader(Holding startHolding, Holding destinationHolding)
    {
        Unit workingUnit = startHolding.Unit;
        startHolding.Unit = null;
        destinationHolding.Unit = workingUnit;
    }
}
