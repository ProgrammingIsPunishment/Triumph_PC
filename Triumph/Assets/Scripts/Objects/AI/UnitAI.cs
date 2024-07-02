using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    public Unit CoupledUnit;
    public List<Holding> NavigationPath { get; set; }

    public UnitAI(Unit unit)
    {
        this.CoupledUnit = unit;
        this.NavigationPath = new List<Holding>();
    }

    public void TakeTurn()
    {
        if (this.CoupledUnit.Dispatch != null)
        {
            switch (this.CoupledUnit.Dispatch.Tasks[0].TaskType)
            {
                case TaskType.Move:
                    Holding destinationHolding = this.NavigationPath[0];
                    this.Move(destinationHolding);
                    this.Claim(destinationHolding);
                    if (NavigationPath.Count == 0) { this.CoupledUnit.Dispatch.Completed(); }
                    break;
            }

            if (this.CoupledUnit.Dispatch.IsCompleted)
            {
                this.CoupledUnit.Dispatch = null;
            }
        }
    }

    public void DeterminePath(Holding currentHolding, Holding destinationHolding)
    {
        this.NavigationPath.Clear();

        int lowestTurnCount = Oberkommando.UTILITIES_SERVICE.DistanceBetweenHoldings(currentHolding,destinationHolding);

        if (lowestTurnCount == 1)
        {
            this.NavigationPath.Add(destinationHolding);
        }
        else
        {
            List<int> xValues = new List<int>() { currentHolding.XPosition, destinationHolding.XPosition };
            List<int> zValues = new List<int>() { currentHolding.ZPosition, destinationHolding.ZPosition };

            int maximumX = xValues.Max();
            int minimumX = xValues.Min();
            int maximumZ = zValues.Max();
            int minimumZ = zValues.Min();

            List<Holding> holdingsToFindPathIn = Oberkommando.SAVE.Holdings.Where(h =>
                (h.XPosition >= minimumX && h.XPosition <= maximumX)
                && (h.ZPosition >= minimumZ && h.ZPosition <= maximumZ)
                && h.TerrainType != TerrainType.Ocean).ToList();

            List<List<Holding>> pathOptions = new List<List<Holding>>();
        }
        

        this.NavigationPath.Add(destinationHolding);

        UnityEngine.Debug.Log(lowestTurnCount);
    }

    private void Move(Holding holding)
    {
        this.CoupledUnit.XPosition = holding.XPosition;
        this.CoupledUnit.ZPosition = holding.ZPosition;

        this.CoupledUnit.CoupledUnitManager.transform.localPosition = new Vector3(this.CoupledUnit.XPosition * 10, 0f, this.CoupledUnit.ZPosition * 10);
    }

    private void Claim(Holding holding)
    {
        holding.Owner = this.CoupledUnit.Owner;
    }
}
