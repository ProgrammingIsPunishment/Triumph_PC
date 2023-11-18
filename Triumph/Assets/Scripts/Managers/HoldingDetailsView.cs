using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public enum HoldingDetailsViewState
{
    Show,
    ShowAdjacentHoldingsForMove,
    HoldingSelectedForMove,
    MoveLeader,
    Hide
}

public class HoldingDetailsView : MonoBehaviour
{
    [SerializeField] private TMP_InputField holdingNameInput;
    [SerializeField] private TMP_InputField unitNameInput;
    [SerializeField] private TextMeshProUGUI unitActionPointText;
    [SerializeField] private GameObject unitContainer;
    [SerializeField] private GameObject noUnitContainer;

    [SerializeField] public SummaryTabManager summaryTabManager;
    [SerializeField] public NaturalResourcesTabManager naturalResourcesTabManager;
    [SerializeField] public UnitTabManager unitTabManager;

    //Action Buttons
    [SerializeField] private GameObject moveActionButton;
    [SerializeField] private GameObject gatherWoodActionButton;

    //Tab buttons
    [SerializeField] private HoldingDetailsTabButton summaryTabButton;
    [SerializeField] private HoldingDetailsTabButton naturalResourcesTabButton;
    [SerializeField] private HoldingDetailsTabButton unitInventoryTabButton;

    public void Set(HoldingDetailsViewState holdingDetailsViewState)
    {
        Holding tempHolding = Oberkommando.UI_CONTROLLER.UIData.HoldingDetails_Holding;
        Unit tempUnit = Oberkommando.UI_CONTROLLER.UIData.HoldingDetails_Unit;
        Holding tempDestinationHolding = Oberkommando.UI_CONTROLLER.UIData.HoldingDetails_DestinationHolding;
        Civilization currentPlayer = Oberkommando.SAVE.AllCivilizations[0];

        switch (holdingDetailsViewState)
        {
            case HoldingDetailsViewState.Show:
                //Just showing the details of the selected holding
                this.Refresh(tempHolding,tempUnit);
                this.Show();
                break;
            case HoldingDetailsViewState.ShowAdjacentHoldingsForMove:
                Oberkommando.UI_CONTROLLER.ShowSelectableAdjacentExplorableHoldings(currentPlayer, tempHolding);
                holdingDetailsViewState = HoldingDetailsViewState.HoldingSelectedForMove;
                break;
            case HoldingDetailsViewState.MoveLeader:
                float tempXPosition = tempDestinationHolding.XPosition;
                float tempZPosition = tempDestinationHolding.ZPosition;
                Oberkommando.UI_CONTROLLER.UIData.HoldingDetails_Unit.Move(tempXPosition,tempZPosition);
                Oberkommando.UI_CONTROLLER.HideSelectableAdjacentExplorableHoldings(currentPlayer, tempHolding);
                currentPlayer.ExploredHoldings.Add(tempDestinationHolding);
                tempDestinationHolding.HoldingDisplayManager.ShowExplored(true);
                tempDestinationHolding.VisibilityLevel = VisibilityLevel.Explored;
                holdingDetailsViewState = HoldingDetailsViewState.Show;
                break;
            case HoldingDetailsViewState.Hide:
                this.Hide();
                holdingDetailsViewState = HoldingDetailsViewState.Show;
                break;
            default:
                break;
        }

        Oberkommando.UI_CONTROLLER.UIData.HoldingDetailsViewState = holdingDetailsViewState;
    }

    private void Refresh(Holding holding,Unit unit)
    {
        this.holdingNameInput.text = holding.DisplayName;

        this.summaryTabManager.Show();
        this.naturalResourcesTabManager.Hide();
        this.unitTabManager.Hide();

        this.EnableTabButtons(holding);

        if (unit != null)
        {
            this.unitNameInput.text = unit.DisplayName;
            this.unitContainer.SetActive(true);
            //this.unitActionPointText.text = unit.ActionPoints.ToString();

            //if (unit.ActionPoints == 0)
            //{
            //    this.moveActionButton.SetActive(false);
            //}
            //else
            //{
            //    this.moveActionButton.SetActive(true);

            //    if (holding.NaturalResourcesInventory.ContainsItem("forest"))
            //    {
            //        this.gatherWoodActionButton.SetActive(true);
            //    }
            //    else
            //    {
            //        this.gatherWoodActionButton.SetActive(false);
            //    }
            //}

            //Unit Tab Manager
            if (unit.SupplyInventory.ResourceItems.Count >= 1)
            {
                this.unitTabManager.UpdateDisplay(unit.SupplyInventory.ResourceItems);
            }
        }
        else
        {
            this.unitContainer.SetActive(false);
        }

        //Summary Tab Manager
        this.summaryTabManager.UpdateView(holding);

        //Natural Resources Tab Manager
        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTabManager.UpdateView(holding.NaturalResourcesInventory.ResourceItems);
        }
    }

    public void SwitchTab(HoldingDetailsTabType holdingDetailsTabType)
    {
        this.summaryTabManager.Hide();
        this.naturalResourcesTabManager.Hide();
        this.unitTabManager.Hide();

        switch (holdingDetailsTabType)
        {
            case HoldingDetailsTabType.Summary:
                this.summaryTabManager.Show();
                break;
            case HoldingDetailsTabType.NaturalResources:
                this.naturalResourcesTabManager.Show();
                break;
            case HoldingDetailsTabType.Unit:
                this.unitTabManager.Show();
                break;
        }
    }

    private void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void EnableTabButtons(Holding holding)
    {
        this.summaryTabButton.Enable();

        this.naturalResourcesTabButton.Disable();
        this.unitInventoryTabButton.Disable();

        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTabButton.Enable();
        }

        //if(holding.Unit != null)
        //{
        //    this.unitInventoryTabButton.Enable();
        //}
    }
}
