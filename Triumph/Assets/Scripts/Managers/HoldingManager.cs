using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingManager : MonoBehaviour
{
    public GameObject terrain;
    public GameObject FogOfWar;
    public GameObject unit;
    public GameObject selection;
    public GameObject selectable;
    public Holding holding;
    public bool isDiscovered;
    public bool isSelectableForMovement;

    public void UpdateFromHolding(Holding holding)
    {
        this.isDiscovered = false;
        this.isSelectableForMovement = false;

        holding.HoldingManager = this;
        this.holding = holding;
    }

    public void ShowDiscovered()
    {
        this.isDiscovered = true;
        this.gameObject.SetActive(true);
        this.terrain.SetActive(true);
        this.FogOfWar.SetActive(false);
    }

    public void ShowExplorable()
    {
        this.gameObject.SetActive(true);
        this.terrain.SetActive(false);
        this.FogOfWar.SetActive(true);
    }

    public void ShowSelectable()
    {
        this.isSelectableForMovement = true;
        this.selectable.SetActive(true);
    }

    public void HideSelectable()
    {
        this.selectable.SetActive(false);
    }

    public void ShowSelected()
    {
        this.selection.SetActive(true);
    }

    public void HideSelected()
    {
        this.selection.SetActive(false);
    }

    public void OnClickEvent()
    {
        switch (Oberkommando.UI_CONTROLLER.UIState)
        {
            case UIState.Default:
                if (this.isDiscovered) 
                {
                    Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails, new UIProcessData(this.holding,HoldingDetailsProcessState.Show));
                }
                break;
            case UIState.HoldingDetails:
                if (this.isDiscovered)
                {
                    Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails, new UIProcessData(this.holding, HoldingDetailsProcessState.Show));
                }
                break;
            case UIState.MoveLeader:
                if (this.isSelectableForMovement)
                {
                    //Oberkommando.UI_CONTROLLER.SelectHoldingForMovement(this.holding);
                    //Oberkommando.LEADER_MOVEMENT_MANAGER.MoveLeader(Oberkommando.SELECTED_HOLDINGS[0], Oberkommando.SELECTED_HOLDINGS[1]);
                    //Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails);
                }
                break;
        }
    }

    public void OnMouseEnter()
    {
        if (this.isSelectableForMovement && Oberkommando.UI_CONTROLLER.UIState == UIState.MoveLeader)
        {
            Oberkommando.UI_CONTROLLER.UpdateSelectionTarget(this,true);
        }
    }

    public void OnMouseExit()
    {
        if (this.isSelectableForMovement && Oberkommando.UI_CONTROLLER.UIState == UIState.MoveLeader)
        {
            Oberkommando.UI_CONTROLLER.UpdateSelectionTarget(this,false);
        }
    }
}
