using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [NonSerialized] public GameMode GameMode;

    [NonSerialized] private Holding SelectedHolding = null;
    [NonSerialized] private Unit SelectedUnit = null;
    [NonSerialized] private List<Holding> SelectableHoldings = new List<Holding>();

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
}
