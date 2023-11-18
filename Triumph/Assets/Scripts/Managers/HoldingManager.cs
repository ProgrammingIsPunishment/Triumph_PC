using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HoldingManager : MonoBehaviour
{
    public GameObject terrain;
    public GameObject FogOfWar;
    public GameObject unitObject;
    public GameObject resourceObject = null;
    public GameObject selection;
    public GameObject selectable;
    public Holding holding;
    public bool isDiscovered;
    public bool isSelectableForMovement;

    public void UpdateFromHolding(Holding holding)
    {
        this.isDiscovered = false;
        this.isSelectableForMovement = false;

        //holding.HoldingManager = this;
        this.holding = holding;
    }

    public void ShowDiscovered()
    {
        this.isDiscovered = true;
        this.gameObject.SetActive(true);
        this.terrain.SetActive(true);
        if (this.resourceObject != null)
        {
            this.resourceObject.SetActive(true);
        }
        this.FogOfWar.SetActive(false);
    }

    public void ShowExplorable()
    {
        this.gameObject.SetActive(true);
        this.terrain.SetActive(false);
        this.FogOfWar.SetActive(true);
    }

    public void ShowSelectable(bool showSelectable)
    {
        if (showSelectable)
        {
            this.isSelectableForMovement = true;
            this.selectable.SetActive(true);
        }
        else
        {
            this.isSelectableForMovement = false;
            this.selectable.SetActive(false);
        }
    }

    public void ShowSelected(bool showSelected)
    {
        if (showSelected)
        {
            this.selection.SetActive(true);
        }
        else
        {
            this.selection.SetActive(false);
        }
    }

    public void OnClickEvent()
    {
        //switch (Oberkommando.UI_CONTROLLER.UIStateStack.Last())
        //{
        //    case UIStateType.HoldingDetails:
        //        if (this.isDiscovered)
        //        {
        //            Oberkommando.UI_CONTROLLER.HoldingDetailsProcess(HoldingDetailsSubState.Show,this.holding);
        //        }
        //        break;
        //    case UIStateType.MoveLeader:
        //        if (this.isSelectableForMovement)
        //        {
        //            Oberkommando.UI_CONTROLLER.MoveLeaderProcess(MoveLeaderSubState.HoldingSelected,this.holding);
        //            //Oberkommando.UI_CONTROLLER.NewUIState(UIState.MoveLeader, new UIProcessData(Oberkommando.UI_CONTROLLER.SelectedHoldings[0], this.holding, MoveLeaderUnitProcessState.Select));
        //        }
        //        break;
        //}
    }

    //public void MoveUnit(HoldingManager destinationHoldingManager)
    //{
    //    destinationHoldingManager.holding.Unit = this.holding.Unit;
    //    destinationHoldingManager.unitObject = this.unitObject;
    //    destinationHoldingManager.unitObject.transform.SetParent(destinationHoldingManager.gameObject.transform);
    //    destinationHoldingManager.unitObject.transform.localPosition = new Vector3(0f, 0f, 0f);
    //    destinationHoldingManager.holding.Unit.Move();
    //    this.holding.Unit = null;
    //    this.unitObject = null;
    //}

    public void Gather()
    {
        //Need to do???
    }

    //public void OnMouseEnter()
    //{
    //    if (this.isSelectableForMovement && Oberkommando.UI_CONTROLLER.UIState == UIState.MoveLeader)
    //    {
    //        //Oberkommando.UI_CONTROLLER.UpdateSelectionTarget(this,true);
    //    }
    //}

    //public void OnMouseExit()
    //{
    //    if (this.isSelectableForMovement && Oberkommando.UI_CONTROLLER.UIState == UIState.MoveLeader)
    //    {
    //        //Oberkommando.UI_CONTROLLER.UpdateSelectionTarget(this,false);
    //    }
    //}
}
