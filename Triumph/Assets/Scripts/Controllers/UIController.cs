using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public List<UIState> UIStateStack { get; set; } = new List<UIState>();

    //Holding Details
    [SerializeField] public HoldingDetailsView holdingDetailsView;

    //Procedures
    [NonSerialized] public HoldingDetailsProcedure HoldingDetailsProcedure = new HoldingDetailsProcedure();
    [NonSerialized] public MoveLeaderProcedure MoveLeaderProcedure = new MoveLeaderProcedure();
    [NonSerialized] public GatherLeaderProcedure GatherLeaderProcedure = new GatherLeaderProcedure();
    [NonSerialized] public ConstructLeaderProcedure ConstructLeaderProcedure = new ConstructLeaderProcedure();

    public void NewUIState(UIState newUIState)
    {
        this.UIStateStack.Add(newUIState);
    }

    public UIState CurrentUIState()
    {
        return this.UIStateStack.Last();
    }

    public void HideAll()
    {
        this.holdingDetailsView.Hide();
    }

    public void MapRefresh(Civilization civilization)
    {
        //Set visibility level to hidden for all holdings to start with
        foreach (Holding h in Oberkommando.SAVE.AllHoldings)
        {
            h.VisibilityLevel = VisibilityLevel.Hidden;
        }

        //Loop through and determine explored holdings
        foreach (Holding h in civilization.ExploredHoldings)
        {
            h.VisibilityLevel = VisibilityLevel.Explored;
            h.HoldingDisplayManager.ShowExplored(true);
            h.HoldingDisplayManager.Show(true);

            foreach (Holding hh in h.AdjacentHoldings)
            {
                if (!civilization.ExploredHoldings.Contains(hh))
                {
                    hh.VisibilityLevel = VisibilityLevel.Unexplored;
                    hh.HoldingDisplayManager.ShowExplored(false);
                    hh.HoldingDisplayManager.Show(true);
                }
            }
        }
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
        }
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
