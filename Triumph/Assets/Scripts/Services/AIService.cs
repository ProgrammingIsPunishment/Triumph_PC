using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIService
{
    public void TakeTurn(Civilization civilization)
    {
        this.CalculateInterest(civilization);

        List<Unit> myUnits = Oberkommando.SAVE.Units.Where(u=>u.Owner.GUID == civilization.GUID).ToList();
        foreach (Unit u in myUnits)
        {
            this.SendUnitOrders(u);
        }
    }

    private void SendUnitOrders(Unit unit)
    {
        Holding holdingAtLocation = Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(unit.XPosition,unit.ZPosition);

        bool hasOrder = false;
        foreach (Holding h in holdingAtLocation.AdjacentHoldings)
        {
            if (!hasOrder)
            {
                if (h.TerrainType != TerrainType.Ocean)
                {
                    if (!this.IsOwner(h, unit.Owner))
                    {
                        Dispatch dispatch = new Dispatch(unit.Name, $"MOVE TO {h.Name}", unit.Owner);
                        Oberkommando.GAME_CONTROLLER.PendingDispatches.Add(dispatch);
                        hasOrder = true;
                    }
                }
            }
        }
    }

    private bool IsOwner(Holding holding, Civilization civilization)
    {
        bool result = false;

        if (holding.Owner != null)
        {
            result = holding.Owner.GUID == civilization.GUID;
        }

        return result;
    }

    private void CalculateInterest(Civilization civilization)
    {
        //List<int> clustersOwned = Oberkommando.VALUELIBRARY.HoldingValueSets.Where(h=>h.).Distinct(v=>v.HoldingCluster);
        foreach (Interest i in civilization.AIProfile.Interests)
        {
            HoldingValueSet holdingValueSet = Oberkommando.VALUELIBRARY.HoldingValueSets.Find(hvs=>hvs.HoldingGUD == i.Holding.GUID);
            //DO I own holdings in this cluster
        }
    }
}
