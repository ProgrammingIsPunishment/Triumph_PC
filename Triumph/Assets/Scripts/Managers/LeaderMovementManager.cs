using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderMovementManager
{
    public void ShowHoldingsWithinRange(Holding holding)
    {
        List<Holding> selectableHoldings = Oberkommando.SAVE.AllHoldings.Where(ah => holding.AdjacentHoldingGUIDs.Contains(ah.GUID)).ToList();
        foreach (Holding h in selectableHoldings) { h.HoldingManager.ShowSelectable(); }
    }

    public void MoveLeader(Holding startHolding, Holding destinationHolding)
    {
        Unit workingUnit = startHolding.Unit;
        startHolding.Unit = null;
        destinationHolding.Unit = workingUnit;
    }
}
