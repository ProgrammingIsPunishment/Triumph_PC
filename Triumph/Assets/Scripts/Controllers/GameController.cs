using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [NonSerialized] public GameMode GameMode;

    [NonSerialized] public Holding SelectedHolding = null;
    [NonSerialized] public Unit SelectedUnit = null;

    public void SelectHolding(Holding selectedHolding)
    {
        if (this.SelectedHolding != null)
        {
            if (this.SelectedHolding.GUID == selectedHolding.GUID)
            {
                this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(false);
                this.SelectedHolding = null;
                this.SelectedUnit = null;
            }
            else
            {
                this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(false);
                this.SelectedHolding = selectedHolding;
                this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(true);
            }
        }
        else
        {
            this.SelectedHolding = selectedHolding;
            this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(true);
        }
    }

    public void SelectUnit(Unit selectedUnit)
    {
        if (this.SelectedUnit != null)
        {
            if (this.SelectedUnit.GUID == selectedUnit.GUID)
            {
                this.SelectedUnit = null;
            }
            else
            {
                this.SelectedUnit = selectedUnit;
            }
        }
        else
        {
            this.SelectedUnit = selectedUnit;
        }
    }
}
