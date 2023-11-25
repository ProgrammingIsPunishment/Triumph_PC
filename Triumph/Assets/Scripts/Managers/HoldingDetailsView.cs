using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class HoldingDetailsView : MonoBehaviour
{
    [SerializeField] private TMP_InputField holdingNameInput;
    [SerializeField] private TMP_InputField unitNameInput;
    [SerializeField] private TextMeshProUGUI unitActionPointText;
    [SerializeField] private GameObject unitContainer;
    [SerializeField] private GameObject noUnitContainer;

    [SerializeField] public SummaryTabManager summaryTabManager;
    [SerializeField] public InventoryView naturalResourcesTab;
    [SerializeField] public InventoryView unitSupplyTab;

    //Action Buttons
    [SerializeField] private GameObject moveActionButton;
    [SerializeField] private GameObject gatherActionButton;

    //Tab buttons
    [SerializeField] private HoldingDetailsTabButton summaryTabButton;
    [SerializeField] private HoldingDetailsTabButton naturalResourcesTabButton;
    [SerializeField] private HoldingDetailsTabButton unitInventoryTabButton;

    public void Refresh(Holding holding, Unit unit)
    {
        this.holdingNameInput.text = holding.DisplayName;

        this.EnableTabButtons(holding, unit);

        if (unit != null)
        {
            this.EnableUnitActionButtons(holding, unit);
            this.unitNameInput.text = unit.DisplayName;
            this.unitContainer.SetActive(true);
            this.unitActionPointText.text = unit.ActionPoints.ToString();

            //Unit Tab View
            if (unit.Inventory.ResourceItems.Count >= 1)
            {
                this.unitSupplyTab.Refresh(unit.Inventory, unit.Supply);
            }
        }
        else
        {
            this.unitContainer.SetActive(false);
        }

        //Summary Tab View
        this.summaryTabManager.UpdateView(holding);

        //Natural Resources Tab View
        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTab.Refresh(holding.NaturalResourcesInventory, unit.Supply);
        }
    }

    public void Default()
    {
        this.summaryTabManager.Show();
        this.naturalResourcesTab.Hide();
        this.unitSupplyTab.Hide();
    }

    public void EnableUnitActionButtons(Holding holding, Unit unit)
    {
        this.moveActionButton.SetActive(false);
        this.gatherActionButton.SetActive(false);

        if (unit.ActionPoints >= 1)
        {
            if (unit != null)
            {
                this.moveActionButton.SetActive(true);
            }

            if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
            {
                this.gatherActionButton.SetActive(true);
            }
        }
    }

    public void SwitchTab(HoldingDetailsTabType holdingDetailsTabType)
    {
        this.summaryTabManager.Hide();
        this.naturalResourcesTab.Hide();
        this.unitSupplyTab.Hide();

        switch (holdingDetailsTabType)
        {
            case HoldingDetailsTabType.Summary:
                this.summaryTabManager.Show();
                break;
            case HoldingDetailsTabType.NaturalResources:
                this.naturalResourcesTab.Show();
                break;
            case HoldingDetailsTabType.Unit:
                this.unitSupplyTab.Show();
                break;
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void EnableTabButtons(Holding holding, Unit unit)
    {
        this.summaryTabButton.Enable();

        this.naturalResourcesTabButton.Disable();
        this.unitInventoryTabButton.Disable();

        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTabButton.Enable();
        }

        if (unit != null)
        {
            this.unitInventoryTabButton.Enable();
        }
    }
}
