using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TMP_InputField unitNameInput;
    [SerializeField] private TextMeshProUGUI unitActionPointText;
    [SerializeField] private TextMeshProUGUI unitPopulationText;
    [SerializeField] private GameObject unitContainer;
    [SerializeField] private GameObject unitStorageContainer;
    [SerializeField] private GameObject unitSupplyContainer;
    [SerializeField] private GameObject noUnitContainer;
    [SerializeField] private GameObject unitTabButtonsContainer;

    [SerializeField] public InventoryView unitInventoryView;

    //Action Buttons
    [SerializeField] private GameObject moveActionButton;
    [SerializeField] private GameObject gatherActionButton;
    [SerializeField] private GameObject buildActionButton;
    [SerializeField] private GameObject laborActionButton;
    [SerializeField] private GameObject settleActionButton;

    public void Refresh(Holding holding, Unit unit)
    {
        if (unit != null)
        {
            this.unitNameInput.text = unit.DisplayName;
            this.unitActionPointText.text = unit.ActionPoints.ToString();
            this.unitPopulationText.text = unit.People.ToString();

            //Update Action Buttons
            this.moveActionButton.SetActive(false);
            this.gatherActionButton.SetActive(false);
            this.buildActionButton.SetActive(false);
            this.laborActionButton.SetActive(false);
            this.settleActionButton.SetActive(false);

            if (unit.Inventory.ResourceItems.Count >= 1)
            {
                this.unitInventoryView.Refresh(unit.Inventory, unit.Supply);
            }

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

                if (holding.HasUndevelopedLots())
                {
                    this.buildActionButton.SetActive(true);
                }

                if (holding.HasUnconstructedBuildings())
                {
                    this.laborActionButton.SetActive(true);
                }

                if (holding.Population.People <= 0 && unit.People >= 1)
                {
                    this.settleActionButton.SetActive(true);
                }
            }

            this.unitContainer.SetActive(true);
            this.unitTabButtonsContainer.SetActive(true);
            this.noUnitContainer.SetActive(false);
        }
        else
        {
            this.unitContainer.SetActive(false);
            this.unitTabButtonsContainer.SetActive(false);
            this.noUnitContainer.SetActive(true);
        }
    }

    public void SwitchTab(UnitTabType unitTabType)
    {
        //Hide all tabs
        this.unitContainer.SetActive(false);
        this.unitStorageContainer.SetActive(false);
        this.unitSupplyContainer.SetActive(false);

        switch (unitTabType)
        {
            case UnitTabType.Summary: this.unitContainer.SetActive(true); break;
            case UnitTabType.Inventory: this.unitStorageContainer.SetActive(true); break;
            case UnitTabType.Supply: this.unitSupplyContainer.SetActive(true); break;
        }
    }

    public void ShowDefaultTab()
    {
        this.SwitchTab(UnitTabType.Summary);
    }
}
