using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject unexploredObject;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject selectableObject;

    [NonSerialized] public GameObject terrainObject = null;
    [NonSerialized] public GameObject resourceObject = null;

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
        switch (Oberkommando.UI_CONTROLLER.CurrentUIState())
        {
            case UIState.HoldingDetails:
                switch (Oberkommando.UI_CONTROLLER.UIData.HoldingDetailsViewState)
                {
                    case HoldingDetailsViewState.Show:
                        //Only allow interaction if the holding is explored
                        if (this.holding.VisibilityLevel == VisibilityLevel.Explored)
                        {
                            Unit tempUnit = this.GetUnitAtThisLocation();
                            Oberkommando.UI_CONTROLLER.UIData.AssignHoldingDetailsData(this.holding, tempUnit);
                            Oberkommando.UI_CONTROLLER.holdingDetailsView.Set(HoldingDetailsViewState.Show);
                        }
                        break;
                    case HoldingDetailsViewState.HoldingSelectedForMove:
                        //Only allow interaction if the holding is explored
                        if (Oberkommando.UI_CONTROLLER.UIData.HoldingDetails_Holding.AdjacentHoldings.Contains(this.holding))
                        {
                            Oberkommando.UI_CONTROLLER.UIData.HoldingDetails_DestinationHolding = this.holding;
                            Oberkommando.UI_CONTROLLER.holdingDetailsView.Set(HoldingDetailsViewState.MoveLeader);
                        }
                        break;
                }
                break;
            //case UIState.MoveLeader:break;
            default: /*Do nothing...*/ break;
        }
    }

    private Unit GetUnitAtThisLocation()
    {
        Unit result = Oberkommando.SAVE.AllUnits.Find(u => u.XPosition == this.holding.XPosition && u.ZPosition == this.holding.ZPosition);

        return result;
    }
}
