using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public List<UIState> UIStateStack { get; set; } = new List<UIState>();
    public List<UIData> UIDataStack { get; set; } = new List<UIData>();

    //Master Views
    [SerializeField] public HoldingView holdingView;

    public void UpdateUIState(UIState newUIState, UIData uiData)
    {
        //Process the new UI state
        switch (newUIState)
        {
            case UIState.HoldingDetails_Show:
                if (this.CurrentUIData().Holding != null)
                {
                    this.holdingView.Refresh(this.CurrentUIData().Holding, this.CurrentUIData().Unit);
                    this.CurrentUIData().Holding.HoldingDisplayManager.ShowSelected(true);
                    this.holdingView.ShowDefaultTab();
                    this.holdingView.Show();
                }
                break;
            case UIState.HoldingDetails_Hide:
                if (this.CurrentUIData().Holding != null)
                {
                    this.CurrentUIData().Holding.HoldingDisplayManager.ShowSelected(false);
                }
                this.holdingView.Hide();
                newUIState = UIState.HoldingDetails_Show;
                break;
            default:
                break;
        }

        this.UIStateStack.Add(newUIState);
    }

    public UIState CurrentUIState()
    {
        return this.UIStateStack.Last();
    }

    public UIData CurrentUIData()
    {
        return this.UIDataStack.Last();
    }

    public void ClearStateAndData()
    {
        this.UIStateStack.Clear();
        this.UIStateStack.Clear();

        this.UIStateStack.Add(UIState.HoldingDetails_Show);
        this.UIStateStack.Add(UIState.HoldingDetails_Show);
    }

    public void HideAll()
    {
        this.holdingView.Hide();
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
