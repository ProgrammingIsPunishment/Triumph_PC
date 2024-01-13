using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HoldingDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject unexploredObject;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject selectableObject;

    [NonSerialized] public GameObject terrainObject = null;
    [NonSerialized] public GameObject resourceObject = null;
    [NonSerialized] public GameObject buildingObject = null;

    [NonSerialized] public Holding holding = null;

    public void Couple(Holding holdingToCouple)
    {
        this.holding = holdingToCouple;
        this.holding.HoldingDisplayManager = this;
    }

    public void Show(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void ShowExplored(bool isExplored)
    {
        if (isExplored)
        {
            //This holding has been explored
            this.unexploredObject.SetActive(false);
            this.terrainObject.SetActive(true);
            if (resourceObject != null)
            {
                this.resourceObject.SetActive(true);
            }
        }
        else
        {
            //This holding has - not - been explored
            this.unexploredObject.SetActive(true);
            this.terrainObject.SetActive(false);
            if (resourceObject != null)
            {
                this.resourceObject.SetActive(false);
            }
        }
    }

    public void ShowSelectable(bool isSelectable)
    {
        if (isSelectable) { this.selectableObject.SetActive(true); }
        else { this.selectableObject.SetActive(false); }
    }

    public void ShowSelected(bool isSelected)
    {
        if (isSelected) { this.selectedObject.SetActive(true); }
        else { this.selectedObject.SetActive(false); }
    }

    public void OnClickEvent()
    {
        Unit tempUnit = null;

        switch (Oberkommando.UI_CONTROLLER.CurrentUIState())
        {
            case UIState.HoldingDetails_Show:
                tempUnit = this.GetUnitAtThisLocation();
                Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.HoldingDetails_Show, new UIData().HoldingDetails(this.holding,tempUnit));

                //Oberkommando.UI_CONTROLLER.holdingView.Refresh(this.holding,tempUnit);
                //this.ShowSelected(true);
                //Oberkommando.UI_CONTROLLER.holdingView.Show();
                break;
            //case UIState.MoveLeader:
            //    //Oberkommando.UI_CONTROLLER.MoveLeaderProcedure.DestinationHolding = this.holding;
            //    //Oberkommando.UI_CONTROLLER.MoveLeaderProcedure.Handle(MoveLeaderProcedureStep.Move);
            //    break;
            default: /*Do nothing...*/ break;
        }
    }

    private Unit GetUnitAtThisLocation()
    {
        Unit result = Oberkommando.SAVE.AllUnits.Find(u => u.XPosition == this.holding.XPosition && u.ZPosition == this.holding.ZPosition);

        return result;
    }
}
