using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIState UIState { get; set; }

    //Holding Details
    public HoldingDetailsManager holdingDetailsManager;
    //public HoldingDetailsSubState HoldingDetailsSubState { get; set; } = HoldingDetailsSubState.None;
    //public Holding HoldingDetailsHolding { get; set; } = null;
    //Move Leader
    //public MoveLeaderSubState MoveLeaderSubState { get; set; } = MoveLeaderSubState.None;

    public void HoldingDetailsProcess(HoldingDetailsSubState holdingDetailsSubState, Holding holding)
    {
        //this.HoldingDetailsSubState = holdingDetailsSubState;

        //switch (holdingDetailsSubState)
        //{
        //    case HoldingDetailsSubState.Show:
        //        //There is already a holding slected...
        //       // if (HoldingDetailsHolding != null) { this.HoldingDetailsHolding.HoldingManager.ShowSelected(false); }

        //        //holding.HoldingManager.ShowSelected(true);
        //        this.holdingDetailsManager.UpdateDisplay(holding);
        //        HoldingDetailsHolding = holding;
        //        this.holdingDetailsManager.Show();
        //        break;
        //    case HoldingDetailsSubState.Hide:
        //        this.holdingDetailsManager.Hide();
        //        //this.HoldingDetailsHolding.HoldingManager.ShowSelected(false);
        //        this.HoldingDetailsHolding = null;
        //        this.HoldingDetailsSubState = HoldingDetailsSubState.None;
        //        break;
        //}

        ////Reset back to default state
        //if (this.HoldingDetailsSubState == HoldingDetailsSubState.None)
        //{
        //    //Do something???? Maybe?
        //}
    }

    public void MoveLeaderProcess(MoveLeaderSubState moveLeaderSubState, Holding holding)
    {
        //this.MoveLeaderSubState = moveLeaderSubState;

        //switch (moveLeaderSubState)
        //{
        //    case MoveLeaderSubState.ShowSelectableHoldings:
        //        this.ShowHoldingsWithinRange(this.HoldingDetailsHolding, true);
        //        break;
        //    case MoveLeaderSubState.HoldingSelected:
        //        bool tempCanMove = false;
        //        //Only move the unit to certain terrains...
        //        switch (holding.TerrainType)
        //        {
        //            case TerrainType.Grassland: tempCanMove = true; break;
        //            case TerrainType.Hills: tempCanMove = true; break;
        //        }

        //        //if (!holding.HoldingManager.isDiscovered)
        //        //{
        //        //    holding.HoldingManager.ShowDiscovered();
        //        //    this.ShowAdjacentExplorableHoldings(holding);
        //        //}

        //        if (tempCanMove)
        //        {
        //            //this.HoldingDetailsHolding.HoldingManager.MoveUnit(holding.HoldingManager);
        //            this.ShowHoldingsWithinRange(this.HoldingDetailsHolding, false);
        //        }
        //        this.MoveLeaderSubState = MoveLeaderSubState.None;
        //        break;
        //}

        ////Reset back to default state
        //if (this.MoveLeaderSubState == MoveLeaderSubState.None)
        //{
        //    this.ShowHoldingsWithinRange(this.HoldingDetailsHolding,false);
        //}
    }

    public void HideAll()
    {
        this.holdingDetailsManager.Hide();
    }

    //public void ShowDiscoveredHoldings(Civilization civilization)
    //{
    //    foreach (Holding h in Oberkommando.SAVE.AllHoldings)
    //    {
    //        //if (h.DiscoveredCivilizationGUIDs.Contains(civilization.GUID))
    //        //{
    //        //    //h.HoldingManager.ShowDiscovered();
    //        //}
    //    }
    //}

    public void ShowExploredHoldings(Civilization civilization)
    {
        foreach (Holding h in civilization.ExploredHoldings)
        {
            h.HoldingDisplayManager.ShowExplored(true);
            h.HoldingDisplayManager.Show(true);
            //if (h.DiscoveredCivilizationGUIDs.Contains(civilization.GUID))
            //{
            //    List<Holding> adjacentHoldings = Oberkommando.SAVE.AllHoldings.Where(ah => h.AdjacentHoldingGUIDs.Contains(ah.GUID)).ToList();
            //    foreach (Holding ah in adjacentHoldings)
            //    {
            //        //ah.HoldingManager.ShowExplorable();
            //    }
            //}
        }
    }

    public void ShowAdjacentExplorableHoldings(Holding holding)
    {
        //List<Holding> adjacentHoldings = Oberkommando.SAVE.AllHoldings.Where(ah => holding.AdjacentHoldingGUIDs.Contains(ah.GUID)).ToList();

        //foreach (Holding ah in adjacentHoldings)
        //{
        //    //if (!ah.HoldingManager.isDiscovered)
        //    //{
        //    //    ah.HoldingManager.ShowExplorable();
        //    //}
        //}
    }

    public void ShowHoldingsWithinRange(Holding holding, bool isBeingShown)
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
