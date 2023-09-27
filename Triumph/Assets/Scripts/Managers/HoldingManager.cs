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
    public bool isSelectable;

    public void UpdateFromHolding(Holding holding)
    {
        holding.HoldingManager = this;
        this.holding = holding;
        this.isDiscovered = false;
        this.isSelectable = false;
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
        this.isSelectable = true;
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
        if (this.isDiscovered && this.isSelectable == false)
        {
            Oberkommando.UI_CONTROLLER.UpdateDisplay(UIType.HoldingDetails, this.holding);
            Oberkommando.UI_CONTROLLER.Show(UIType.HoldingDetails);
            Oberkommando.UI_CONTROLLER.SetUIState(UIState.HoldingDetails);
        }
    }

    public void OnMouseEnter()
    {
        if (this.isSelectable && Oberkommando.UISTATE == UIState.MoveLeader)
        {
            Oberkommando.UI_CONTROLLER.ShowSelectionTarget(this);
        }
    }
}
