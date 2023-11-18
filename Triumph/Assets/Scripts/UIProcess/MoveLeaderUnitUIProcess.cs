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

public class MoveLeaderUnitUIProcess //: IUIProcess
{
    //public UIState UIState { get; } = UIState.MoveLeader;
    //private MoveLeaderUnitProcessState moveLeaderUnitProcessState = MoveLeaderUnitProcessState.ShowSelectable;
    //private UIProcessData currentUIProcessData;

    public MoveLeaderUnitUIProcess() { }

    public void Process(UIProcessData uIProcessData)
    {
        //this.currentUIProcessData = uIProcessData;
        //this.moveLeaderUnitProcessState = (MoveLeaderUnitProcessState)uIProcessData.MoveLeaderUnitProcessState;

        //switch (this.moveLeaderUnitProcessState)
        //{
        //    case MoveLeaderUnitProcessState.ShowSelectable:
        //        this.ShowHoldingsWithinRange(uIProcessData.Holding, true);
        //        break;
        //    case MoveLeaderUnitProcessState.Select:
        //        bool tempCanMove = false;
        //        //Only move the unit to certain terrains...
        //        switch (uIProcessData.Holding2.TerrainType)
        //        {
        //            case TerrainType.Grassland: tempCanMove = true; break;
        //            case TerrainType.Hills: tempCanMove = true; break;
        //        }
                
        //        if (!uIProcessData.Holding2.HoldingManager.isDiscovered) 
        //        {
        //            uIProcessData.Holding2.HoldingManager.ShowDiscovered();
        //            uIProcessData.Holding2.HoldingManager.ShowAdjacentExplorableHoldings();
        //        }

        //        if (tempCanMove)
        //        {
        //            uIProcessData.Holding.HoldingManager.MoveUnit(uIProcessData.Holding2.HoldingManager);
        //            Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails, new UIProcessData(uIProcessData.Holding2, HoldingDetailsProcessState.Update, null));
        //            this.ShowHoldingsWithinRange(uIProcessData.Holding, false);
        //        }
        //        this.ProcessEnd();
        //        break;
        //}
    }

    public void ProcessEnd()
    {
        ////Logic for reseting the ui process
        //this.moveLeaderUnitProcessState = MoveLeaderUnitProcessState.None;

        //if (this.currentUIProcessData != null)
        //{
        //    this.ShowHoldingsWithinRange(this.currentUIProcessData.Holding, false);
        //    this.currentUIProcessData = null;
        //}

        //Oberkommando.UI_CONTROLLER.ActiveUIStates.Remove(this.UIState);
        //Oberkommando.UI_CONTROLLER.RefocusUIState();
    }

    private void ShowHoldingsWithinRange(Holding holding, bool isBeingShown)
    {
        //List<Holding> selectableHoldings = Oberkommando.SAVE.AllHoldings.Where(ah => holding.AdjacentHoldingGUIDs.Contains(ah.GUID)).ToList();

        if (isBeingShown)
        {
            //foreach (Holding h in selectableHoldings) { h.HoldingManager.ShowSelectable(true); }
        }
        else
        {
            //foreach (Holding h in selectableHoldings) { h.HoldingManager.ShowSelectable(false); }
        }
    }
}
