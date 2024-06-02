using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingManager : MonoBehaviour
{
    [SerializeField] private GameObject unexploredObject;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject selectableObject;
    [SerializeField] private GameObject borderObject;

    [SerializeField] private GameObject debug_ChokePointObject;

    [NonSerialized] public GameObject terrainObject = null;

    private Holding CoupledHolding = null;

    public void OnClickEvent()
    {
        if (Oberkommando.DEBUG.IsDebugMode) { UnityEngine.Debug.Log($"{this.CoupledHolding.Name} X:{this.CoupledHolding.XPosition} Z:{this.CoupledHolding.ZPosition}"); }

        if (Oberkommando.GAME_CONTROLLER.GameMode == GameMode.Selection)
        {
            Unit unitAtLocation = Oberkommando.UTILITIES_SERVICE.GetUnitAtLocation(this.CoupledHolding.XPosition, this.CoupledHolding.ZPosition);
            Oberkommando.GAME_CONTROLLER.Select(this.CoupledHolding, unitAtLocation);
        }
    }

    public void Couple(Holding holding) 
    {
        this.CoupledHolding = holding;
        this.CoupledHolding.CoupledHoldingDisplay = this;
    }

    public void ShowDiscovered()
    {
        this.unexploredObject.SetActive(false);
        this.terrainObject.SetActive(true);
    }

    public void ShowSelected(bool isBeingShown)
    {
        this.selectedObject.SetActive(isBeingShown);
    }

    public void ShowSelectable(bool isBeingShown)
    {
        this.selectableObject.SetActive(isBeingShown);
    }

    public void ShowBorder(Color color)
    {
        this.borderObject.GetComponentInChildren<Image>().color = color;
        this.borderObject.SetActive(true);
    }

    public List<Holding> GetHoldingsForMovement()
    {
        List<Holding> result = new List<Holding>();

        foreach (Holding h in this.CoupledHolding.AdjacentHoldings)
        {
            if (h.TerrainType != TerrainType.Ocean)
            {
                result.Add(h);
            }
        }

        return result;
    }

    public void Degbug()
    {
        if (Oberkommando.DEBUG.ShowChokePoints) {
            this.debug_ChokePointObject.SetActive(Oberkommando.DEBUG.ShowCokePoint(this.CoupledHolding));
        }
    }

    //public void ShowAdjacentHoldings(bool isBeingShown)
    //{
    //    foreach (Holding h in this.CoupledHolding.AdjacentHoldings)
    //    {
    //        if (h.TerrainType != TerrainType.Ocean)
    //        {
    //            h.CoupledHoldingDisplay.ShowSelectable(isBeingShown);
    //            Oberkommando.GAME_CONTROLLER.AddSelectableHolding(h);
    //        }
    //    }
    //}
}