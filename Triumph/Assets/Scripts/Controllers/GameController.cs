using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [NonSerialized] public GameMode GameMode;
    [NonSerialized] public bool CanCameraMove;

    [NonSerialized] private Holding SelectedHolding = null;
    [NonSerialized] private Unit SelectedUnit = null;
    [NonSerialized] private List<Holding> SelectableHoldings = new List<Holding>();

    [NonSerialized] public List<Dispatch> PendingDispatches = new List<Dispatch>();

    public void Select(Holding selectedHolding, Unit selectedUnit)
    {
        //Unmark existing
        if (this.SelectedUnit != null)
        {
            //this.SelectedHolding.CoupledHoldingDisplay.ShowAdjacentHoldings(false);
            //foreach (Holding h in this.SelectableHoldings) { h.CoupledHoldingDisplay.ShowSelectable(false); }
            this.SelectableHoldings.Clear();
        }
        if (this.SelectedHolding != null)
        {
            this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(false);
        }

        //Mark new
        this.SelectedHolding = selectedHolding;
        this.SelectedUnit = selectedUnit;
        this.SelectableHoldings.Clear();

        if (this.SelectedHolding != null)
        {
            this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(true);
        }
        if (this.SelectedUnit != null)
        {
            //this.SelectedHolding.CoupledHoldingDisplay.ShowAdjacentHoldings(true);
            //this.SelectableHoldings.AddRange(this.SelectedHolding.CoupledHoldingDisplay.GetHoldingsForMovement());
            //foreach (Holding h in this.SelectableHoldings) { h.CoupledHoldingDisplay.ShowSelectable(true); }
        }


        Oberkommando.UI_CONTROLLER.HoldingDetailsManager.Refresh(selectedHolding, selectedUnit);
        Oberkommando.UI_CONTROLLER.HoldingDetailsManager.Show(true);
    }

    public void EndTurn()
    {
        this.ProcessDispatches();
        this.ProcessUnitActions();
        this.UpdateBorders();

        Oberkommando.UI_CONTROLLER.DispatchesManager.Default();

        Oberkommando.SAVE.Turn++;
        Debug.Log(Oberkommando.SAVE.Turn);
    }

    public void ProcessDispatches()
    {
        //Process Dispatches to set tasks
        foreach (Dispatch d in this.PendingDispatches)
        {
            Oberkommando.DISPATCHES_CONTROLLER.Process(d);
            Oberkommando.SAVE.Units.Find(u=>u.Name.ToUpper() == d.Recipient.ToUpper()).Dispatches.Add(d);
            d.Received();
        }

        Oberkommando.SAVE.Dispatches.AddRange(this.PendingDispatches);
        this.PendingDispatches.Clear();
    }

    //Tornado 5/21/2024
    public void ProcessUnitActions()
    {
        //Process Dispatches to set tasks
        foreach (Unit u in Oberkommando.SAVE.Units)
        {
            u.TakeAction();
        }
    }

    public void UpdateBorders()
    {
        foreach (Holding h in Oberkommando.SAVE.Holdings)
        {
            if (h.Owner != null)
            {
                h.CoupledHoldingDisplay.ShowBorder(h.Owner.Color);
            }
        }
    }
}
