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
    [SerializeField] public NaturalResourcesTabManager naturalResourcesTabManager;
    [SerializeField] public UnitTabManager unitTabManager;

    //Action Buttons
    [SerializeField] private GameObject moveActionButton;
    [SerializeField] private GameObject gatherWoodActionButton;

    //Tab buttons
    [SerializeField] private HoldingDetailsTabButton summaryTabButton;
    [SerializeField] private HoldingDetailsTabButton naturalResourcesTabButton;
    [SerializeField] private HoldingDetailsTabButton unitInventoryTabButton;

    public void Refresh(Holding holding, Unit unit)
    {
        this.holdingNameInput.text = holding.DisplayName;

        this.summaryTabManager.Show();
        this.naturalResourcesTabManager.Hide();
        this.unitTabManager.Hide();

        this.EnableTabButtons(holding, unit);

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
