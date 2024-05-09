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

    [NonSerialized] public GameObject terrainObject = null;

    private Holding CoupledHolding = null;

    public void OnClickEvent()
    {
        if (Oberkommando.ISDEBUGMODE) { Debug.Log(this.CoupledHolding.Name); }

        if (Oberkommando.GAME_CONTROLLER.GameMode == GameMode.Selection)
        {
            Unit unitAtLocation = Oberkommando.UTILITIES_SERVICE.GetUnitAtLocation(this.CoupledHolding.XPosition, this.CoupledHolding.ZPosition);

            Oberkommando.GAME_CONTROLLER.SelectHolding(this.CoupledHolding);
            Oberkommando.GAME_CONTROLLER.SelectUnit(unitAtLocation);
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

    public void ShowBorder(Color color)
    {
        this.borderObject.GetComponentInChildren<Image>().color = color;
        this.borderObject.SetActive(true);
    }
}