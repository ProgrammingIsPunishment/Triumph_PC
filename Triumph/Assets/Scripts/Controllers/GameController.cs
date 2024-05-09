using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [NonSerialized] public GameMode GameMode;

    [NonSerialized] private Holding SelectedHolding = null;
    [NonSerialized] private Unit SelectedUnit = null;

    public void Select(Holding selectedHolding, Unit selectedUnit)
    {
        if (this.SelectedUnit != null)
        {
            if (selectedUnit != null)
            {
                //There is a unit that will be selected
                if (this.SelectedUnit.GUID == selectedUnit.GUID)
                {
                    //This is the same unit that is being selected
                    this.SelectedUnit = null;
                    this.SelectedHolding.CoupledHoldingDisplay.ShowAdjacentHoldings(false);
                }
                else
                {
                    //This is a new unit being selected
                    this.SelectedHolding.CoupledHoldingDisplay.ShowAdjacentHoldings(false);
                    this.SelectedUnit = selectedUnit;
                    selectedHolding.CoupledHoldingDisplay.ShowAdjacentHoldings(true);
                }
            }
            else
            {
                //No holding will be selected
                this.SelectedUnit = null;
                this.SelectedHolding.CoupledHoldingDisplay.ShowAdjacentHoldings(false);
            }
        }
        else
        {
            if (selectedUnit != null)
            {
                this.SelectedUnit = selectedUnit;
                selectedHolding.CoupledHoldingDisplay.ShowAdjacentHoldings(true);
            }
        }

        if (this.SelectedHolding != null)
        {
            //There is an existing holding selected
            if (this.SelectedHolding.GUID == selectedHolding.GUID)
            {
                //This is the same holding that is already selected 
                this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(false);
                this.SelectedHolding = null;
                this.SelectedUnit = null;
            }
            else
            {
                //This is a different holding than the currently selected one
                this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(false);
                this.SelectedHolding = selectedHolding;
                this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(true);
            }
        }
        else
        {
            //New Holding Selected...no currently selected holding
            this.SelectedHolding = selectedHolding;
            this.SelectedHolding.CoupledHoldingDisplay.ShowSelected(true);
        }
    }
}
