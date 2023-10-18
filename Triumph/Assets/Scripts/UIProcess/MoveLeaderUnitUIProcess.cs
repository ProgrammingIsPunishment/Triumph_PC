using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MoveLeaderUnitProcessState
{
    None,
    ShowSelectable,
    Select
}

public class MoveLeaderUnitUIProcess : IUIProcess
{
    public UIState UIState { get; } = UIState.MoveLeader;
    private MoveLeaderUnitProcessState moveLeaderUnitProcessState = MoveLeaderUnitProcessState.ShowSelectable;

    public MoveLeaderUnitUIProcess() { }

    public void Process(UIProcessData uIProcessData)
    {
        this.moveLeaderUnitProcessState = (MoveLeaderUnitProcessState)uIProcessData.MoveLeaderUnitProcessState;

        switch (this.moveLeaderUnitProcessState)
        {
            case MoveLeaderUnitProcessState.ShowSelectable:
                this.HoldingsWithinRange(uIProcessData.Holding, true);
                break;
            case MoveLeaderUnitProcessState.Select:
                uIProcessData.Holding.HoldingManager.MoveUnit(uIProcessData.Holding2.HoldingManager);
                Oberkommando.UI_CONTROLLER.SelectedHoldings[0].HoldingManager.HideSelected();
                Oberkommando.UI_CONTROLLER.SelectedHoldings.Clear();
                this.HoldingsWithinRange(uIProcessData.Holding, false);
                break;
        }
    }

    public void Reset()
    {
        //Logic for reseting the ui process
        //this.HoldingDetailsManager.Hide();
        this.moveLeaderUnitProcessState = MoveLeaderUnitProcessState.None;
    }

    private void HoldingsWithinRange(Holding holding, bool isBeingShown)
    {
        List<Holding> selectableHoldings = Oberkommando.SAVE.AllHoldings.Where(ah => holding.AdjacentHoldingGUIDs.Contains(ah.GUID)).ToList();

        if (isBeingShown)
        {
            foreach (Holding h in selectableHoldings) { h.HoldingManager.DisplaySelectable(true); }
        }
        else
        {
            foreach (Holding h in selectableHoldings) { h.HoldingManager.DisplaySelectable(false); }
        }
    }
}
