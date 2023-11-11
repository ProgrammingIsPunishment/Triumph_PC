using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldingDetailsManager : MonoBehaviour
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

    public void UpdateDisplay(Holding holding)
    {
        this.holdingNameInput.text = holding.DisplayName;

        this.summaryTabManager.Show();
        this.naturalResourcesTabManager.Hide();
        this.unitTabManager.Hide();

        this.EnableTabButtons(holding);

        if (holding.Unit != null)
        {
            this.unitNameInput.text = holding.Unit.DisplayName;
            this.unitContainer.SetActive(true);
            this.unitActionPointText.text = holding.Unit.ActionPoints.ToString();

            if (holding.Unit.ActionPoints == 0)
            {
                this.moveActionButton.SetActive(false);
            }
            else
            {
                this.moveActionButton.SetActive(true);

                if (holding.NaturalResourcesInventory.ContainsItem("forest"))
                {
                    this.gatherWoodActionButton.SetActive(true);
                }
                else
                {
                    this.gatherWoodActionButton.SetActive(false);
                }
            }

            //Unit Tab Manager
            if (holding.Unit.SupplyInventory.ResourceItems.Count >= 1)
            {
                this.unitTabManager.UpdateDisplay(holding.Unit.SupplyInventory.ResourceItems);
            }
        }
        else
        {
            this.unitContainer.SetActive(false);
        }

        //Summary Tab Manager
        this.summaryTabManager.UpdateDisplay(holding);

        //Natural Resources Tab Manager
        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTabManager.UpdateDisplay(holding.NaturalResourcesInventory.ResourceItems);
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

    private void EnableTabButtons(Holding holding)
    {
        this.summaryTabButton.Enable();

        this.naturalResourcesTabButton.Disable();
        this.unitInventoryTabButton.Disable();

        if (holding.NaturalResourcesInventory.ResourceItems.Count >= 1)
        {
            this.naturalResourcesTabButton.Enable();
        }

        if(holding.Unit != null)
        {
            this.unitInventoryTabButton.Enable();
        }
    }
}
