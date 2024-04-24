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
    [SerializeField] public ImprovementsView improvementsTab;
    [SerializeField] public UnitView unitView;
    [SerializeField] public PopulationView populationTab;
    [SerializeField] public InventoryView storageTab;

    //Tab buttons
    [SerializeField] private HoldingDetailsTabButton summaryTabButton;
    [SerializeField] private HoldingDetailsTabButton naturalResourcesTabButton;
    [SerializeField] private HoldingDetailsTabButton unitInventoryTabButton;
    [SerializeField] private HoldingDetailsTabButton improvementsTabButton;
    [SerializeField] private HoldingDetailsTabButton populationTabButton;
    [SerializeField] private HoldingDetailsTabButton storageTabButton;

    [SerializeField] public CloseHoldingViewButton closeHoldingViewButton;

    public void Refresh(Holding holding, Unit unit)
    {
        Oberkommando.COLDSTORAGE_CONTROLLER.ReturnAllInventoryItemViews();

        this.holdingNameInput.text = holding.DisplayName;

        //Views
        this.unitView.Refresh(holding,unit);
        this.summaryTabManager.UpdateView(holding);
        this.improvementsTab.Refresh(holding);
        this.populationTab.Refresh(holding.Population);
        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTab.Couple(holding.NaturalResourcesInventory);
            this.naturalResourcesTab.Refresh(holding.NaturalResourcesInventory);
        }
        //if (holding.StorageInventory.ResourceItems.Count >= 1)
        //{
        //    this.storageTab.Couple(holding.StorageInventory);
        //    this.storageTab.Refresh(holding.StorageInventory, holding.Population.GoodsTemplate);
        //}
        this.storageTab.Couple(holding.StorageInventory);
        this.storageTab.Refresh(holding.StorageInventory, holding.Population.GoodsTemplate);
        if (unit != null)
        {
            //if (unit.Inventory.ResourceItems.Count >= 1)
            //{
            //    this.unitSupplyTab.Refresh(unit.Inventory, unit.Supply);
            //}
        }

        //Tab Buttons
        this.summaryTabButton.Enable();
        this.improvementsTabButton.Enable();
        this.populationTabButton.Enable();
        this.naturalResourcesTabButton.Disable();
        this.storageTabButton.Enable();
        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTabButton.Enable();
        }
        //if (holding.StorageInventory.ResourceItems.Count >= 1)
        //{
        //    this.storageTabButton.Enable();
        //}
    }

    public void SwitchTab(HoldingDetailsTabType holdingDetailsTabType)
    {
        this.summaryTabManager.Hide();
        this.naturalResourcesTab.Hide();
        this.improvementsTab.Hide();
        this.populationTab.Hide();
        this.storageTab.Hide();

        switch (holdingDetailsTabType)
        {
            case HoldingDetailsTabType.Summary:
                this.summaryTabManager.Show();
                break;
            case HoldingDetailsTabType.NaturalResources:
                this.naturalResourcesTab.Show();
                break;
            case HoldingDetailsTabType.Improvements:
                this.improvementsTab.Show();
                break;
            case HoldingDetailsTabType.Population:
                this.populationTab.Show();
                break;
            case HoldingDetailsTabType.Storage:
                this.storageTab.Show();
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

    public void Display(bool isBeingShown)
    {
        if (isBeingShown)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            this.improvementsTab.ShowImprovableLots(false);
        }
    }
}
