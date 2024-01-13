using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class HoldingView : MonoBehaviour
{
    [SerializeField] private TMP_InputField holdingNameInput;

    //Views
    [SerializeField] public SummaryTabManager summaryTabManager;
    [SerializeField] public InventoryView naturalResourcesTab;
    [SerializeField] public InventoryView unitSupplyTab;
    [SerializeField] public ImprovementsView improvementsTab;
    [SerializeField] public UnitView unitView;

    //Tab buttons
    [SerializeField] private HoldingDetailsTabButton summaryTabButton;
    [SerializeField] private HoldingDetailsTabButton naturalResourcesTabButton;
    [SerializeField] private HoldingDetailsTabButton unitInventoryTabButton;
    [SerializeField] private HoldingDetailsTabButton improvementsTabButton;

    public void Refresh(Holding holding, Unit unit)
    {
        this.holdingNameInput.text = holding.DisplayName;

        //Views
        this.unitView.Refresh(holding,unit);
        this.summaryTabManager.UpdateView(holding);
        this.improvementsTab.Refresh(holding);
        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTab.Refresh(holding.NaturalResourcesInventory, unit.Supply);
        }
        if (unit != null)
        {
            if (unit.Inventory.ResourceItems.Count >= 1)
            {
                this.unitSupplyTab.Refresh(unit.Inventory, unit.Supply);
            }
        }

        //Tab Buttons
        this.summaryTabButton.Enable();
        this.improvementsTabButton.Enable();
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

    public void SwitchTab(HoldingDetailsTabType holdingDetailsTabType)
    {
        this.summaryTabManager.Hide();
        this.naturalResourcesTab.Hide();
        this.unitSupplyTab.Hide();
        this.improvementsTab.Hide();

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
            case HoldingDetailsTabType.Improvements:
                this.improvementsTab.Show();
                break;
        }
    }

    public void ShowDefaultTab()
    {
        this.SwitchTab(HoldingDetailsTabType.Summary);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
