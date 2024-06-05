using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIService
{
    public void TakeTurn(Civilization civilization)
    {
        List<IAIGoal> activeGoals = civilization.AICivilizationProfile.AIGoals.Where(aig=>aig.IsActive == true).ToList();
        if (activeGoals.Count() == 0) { civilization.AICivilizationProfile.NewGoal(civilization); }

        IAIGoal activeGoal = civilization.AICivilizationProfile.AIGoals.First(aig => aig.IsActive == true);

        this.WorkTowardsGoal(activeGoal,civilization);
    }

    private void WorkTowardsGoal(IAIGoal goal, Civilization civilization)
    {
        switch (goal)
        {
            case AITerritorialExpansionGoal:
                this.ProcessAITerritorialExpansionGoal((AITerritorialExpansionGoal)goal,civilization);
                break;
            default:
                break;
        }
    }

    private void ProcessAITerritorialExpansionGoal(AITerritorialExpansionGoal goal, Civilization civilization)
    {
        List<Unit> units = Oberkommando.SAVE.Units.Where(u => u.Owner.GUID == civilization.GUID).ToList();

        List<Holding> tempHoldingsToCapture = goal.HoldingsToCapture;

        foreach (Unit u in units)
        {
            if (tempHoldingsToCapture.Count() >= 1)
            {
                this.SendDispatchToUnit(u, $"MOVE TO {tempHoldingsToCapture[0].Name}",civilization);
                tempHoldingsToCapture.RemoveAt(0);
            }
        }
    }

    private void SendDispatchToUnit(Unit unit, string message, Civilization owner)
    {
        Dispatch dispatch = new Dispatch(unit.Name, message, owner);
        Oberkommando.GAME_CONTROLLER.PendingDispatches.Add(dispatch);
    }


    //private void SendUnitOrders(Unit unit)
    //{
    //    Holding holdingAtLocation = Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(unit.XPosition,unit.ZPosition);

    //    bool hasOrder = false;
    //    foreach (Holding h in holdingAtLocation.AdjacentHoldings)
    //    {
    //        if (!hasOrder)
    //        {
    //            if (h.TerrainType != TerrainType.Ocean)
    //            {
    //                if (!this.IsOwner(h, unit.Owner))
    //                {
    //                    Dispatch dispatch = new Dispatch(unit.Name, $"MOVE TO {h.Name}", unit.Owner);
    //                    Oberkommando.GAME_CONTROLLER.PendingDispatches.Add(dispatch);
    //                    hasOrder = true;
    //                }
    //            }
    //        }
    //    }
    //}

    //private bool IsOwner(Holding holding, Civilization civilization)
    //{
    //    bool result = false;

    //    if (holding.Owner != null)
    //    {
    //        result = holding.Owner.GUID == civilization.GUID;
    //    }

    //    return result;
    //}
}
