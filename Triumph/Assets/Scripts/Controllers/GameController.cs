using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        foreach (Civilization c in Oberkommando.SAVE.Civilizations)
        {
            if (c.GUID != Oberkommando.PLAYER.GUID) { Oberkommando.AI_SERVICE.TakeTurn(c); }
        }

        this.ProcessDispatches();
        this.ProcessUnitActions();
        this.ProcessHoldingActions();
        this.UpdateBorders();
        this.CalculateIncome();
        this.CalculateExpenses();
        this.ResetUI(Oberkommando.PLAYER);

        Oberkommando.SAVE.Turn++;
        UnityEngine.Debug.Log(Oberkommando.SAVE.Turn);
    }

    public void ResetUI(Civilization civilization)
    {
        Oberkommando.UI_CONTROLLER.DispatchesManager.Default();

        Oberkommando.UI_CONTROLLER.CoinButton.Refresh(civilization.Coins);
    }

    public void ProcessDispatches()
    {
        //Process Dispatches to set tasks
        foreach (Dispatch d in this.PendingDispatches)
        {
            Oberkommando.DISPATCHES_CONTROLLER.Process(d);

            switch (d.RecipientType)
            {
                case RecipientType.Unit:
                    Oberkommando.SAVE.Units.Find(u => u.Name.ToUpper() == d.Recipient.ToUpper()).Dispatches.Add(d);
                    break;
                case RecipientType.Holding:
                    Oberkommando.SAVE.Holdings.Find(h => h.Name.ToUpper() == d.Recipient.ToUpper()).Dispatches.Add(d);
                    break;
            }

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

    public void ProcessHoldingActions()
    {
        //Process Dispatches to set tasks
        foreach (Holding h in Oberkommando.SAVE.Holdings)
        {
            h.TakeAction();
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

    public void CalculateIncome()
    {
        foreach (Civilization c in Oberkommando.SAVE.Civilizations)
        {
            List<Holding> ownedHoldings = new List<Holding>();

            foreach (Holding h in Oberkommando.SAVE.Holdings)
            {
                if (h.Owner != null) 
                {
                    if (h.Owner.GUID == c.GUID) { ownedHoldings.Add(h); }
                }
            }

            int income = ownedHoldings.Count();

            c.Coins += income;
        }
    }

    public void CalculateExpenses()
    {
        foreach (Civilization c in Oberkommando.SAVE.Civilizations)
        {
            List<Unit> ownedUnits = new List<Unit>();

            foreach (Unit u in Oberkommando.SAVE.Units)
            {
                if (u.Owner != null)
                {
                    if (u.Owner.GUID == c.GUID) { ownedUnits.Add(u); }
                }
            }

            int expenses = ownedUnits.Count();

            c.Coins -= expenses;
        }
    }
}
