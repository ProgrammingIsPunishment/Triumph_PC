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
    [SerializeField] private GameObject borderObject;

    [NonSerialized] public GameObject terrainObject = null;
    [NonSerialized] public GameObject resourceObject = null;
    [NonSerialized] public GameObject buildingObject1 = null;
    [NonSerialized] public GameObject buildingObject2 = null;
    [NonSerialized] public GameObject buildingObject3 = null;
    [NonSerialized] public GameObject buildingObject4 = null;

    [NonSerialized] public Holding holding = null;

    [NonSerialized] private bool IsSelectableForMovement = false;

    public event EventHandler OnHoldingClickForSelection;
    public event EventHandler OnHoldingClickForMovement;

    public void Initialize()
    {
        Oberkommando.UI_CONTROLLER.OnNewHoldingClickForSelection += EventHandler_NewHoldingClickForSelection;
    }

    private void EventHandler_NewHoldingClickForSelection(object sender, EventArgs eventArgs)
    {
        this.ShowSelected(false);
    }

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
            //this.borderObject.SetActive(true);
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
        if (isSelectable) 
        { 
            this.selectableObject.SetActive(true);
            this.IsSelectableForMovement = true;
        }
        else 
        { 
            this.selectableObject.SetActive(false);
            this.IsSelectableForMovement = false;
        }
    }

    public void ShowSelected(bool isSelected)
    {
        if (isSelected) { this.selectedObject.SetActive(true); }
        else { this.selectedObject.SetActive(false); }
    }

    public void ShowBorder(bool isBeingShown)
    {
        this.borderObject.SetActive(isBeingShown);
    }

    public void OnClickEvent()
    {
        if (this.IsSelectableForMovement)
        {
            OnHoldingClickForMovement?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnHoldingClickForSelection?.Invoke(this, EventArgs.Empty);
        }


        //Unit tempUnit = null;

        //switch (Oberkommando.UI_CONTROLLER.CurrentUIState())
        //{
        //    case UIState.HoldingDetails_SelectHolding:
        //        tempUnit = this.GetUnitAtThisLocation();
        //        Oberkommando.UI_CONTROLLER.HoldingDetailsData(this.holding,tempUnit);
        //        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.HoldingDetails_SelectHolding);
        //        break;
        //    case UIState.LeaderMove_SelectHolding:
        //        if (this.IsSelectableForMovement)
        //        {
        //            Oberkommando.UI_CONTROLLER.LeaderMoveData(this.holding);
        //            Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderMove_End);
        //        }
        //        break;
        //    default: 
        //        /*Do nothing...*/ 
        //        break;
        //}
    }

    public void UpdateBuildingModel(int lot, GameObject modelObject)
    {
        switch (lot)
        {
            case 1:
                if (this.buildingObject1 != null) { Destroy(this.buildingObject1); }
                this.buildingObject1 = modelObject; 
                break;
            case 2:
                if (this.buildingObject2 != null) { Destroy(this.buildingObject2); }
                this.buildingObject2 = modelObject;
                break;
            case 3:
                if (this.buildingObject3 != null) { Destroy(this.buildingObject3); }
                this.buildingObject3 = modelObject;
                break;
            case 4:
                if (this.buildingObject4 != null) { Destroy(this.buildingObject4); }
                this.buildingObject4 = modelObject;
                break;
        }
    }

    public Unit GetUnitAtThisLocation()
    {
        Unit result = Oberkommando.SAVE.AllUnits.Find(u => u.XPosition == this.holding.XPosition && u.ZPosition == this.holding.ZPosition);

        return result;
    }
}
