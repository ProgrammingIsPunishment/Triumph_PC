using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TMP_InputField unitNameInput;
    [SerializeField] private TextMeshProUGUI unitActionPointText;
    //[SerializeField] private TextMeshProUGUI unitPopulationText;
    [SerializeField] private GameObject unitContainer;
    [SerializeField] private GameObject unitStorageContainer;
    [SerializeField] private GameObject unitSupplyContainer;
    [SerializeField] private GameObject noUnitContainer;
    [SerializeField] private GameObject unitTabButtonsContainer;
    [NonSerialized] private List<PopSlotView> popSlotViews = new List<PopSlotView>();

    [SerializeField] public InventoryView unitInventoryView;

    //Action Buttons
    [SerializeField] public MoveLeaderButton moveLeaderButton;
    [SerializeField] public ClaimLeaderButton claimLeaderButton;
    [SerializeField] public BuildLeaderButton buildLeaderButton;

    [SerializeField] public GameObject moveActionButton;
    [SerializeField] private GameObject gatherActionButton;
    [SerializeField] private GameObject buildActionButton;
    [SerializeField] private GameObject laborActionButton;
    [SerializeField] private GameObject settleActionButton;
    [SerializeField] private GameObject claimActionButton;

    public void Initialize()
    {
        this.popSlotViews = this.gameObject.GetComponentsInChildren<PopSlotView>().ToList();
    }

    public void Refresh(Holding holding, Unit unit)
    {
        if (unit != null)
        {
            this.unitNameInput.text = unit.DisplayName;
            this.unitActionPointText.text = unit.ActionPoints.ToString();
            //this.unitPopulationText.text = unit.Population.ToString();

            //Update Action Buttons
            this.moveActionButton.SetActive(false);
            this.gatherActionButton.SetActive(false);
            this.buildActionButton.SetActive(false);
            this.laborActionButton.SetActive(false);
            this.settleActionButton.SetActive(false);
            this.claimActionButton.SetActive(false);

            if (unit.Inventory.ResourceItems.Count >= 1)
            {
                this.unitInventoryView.Couple(unit.Inventory);
                this.unitInventoryView.Refresh(unit.Inventory, unit);
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

                if (unit.Population.Pops.Count >= 1 && Oberkommando.SAVE.AllCivilizations[0].OwnsHolding(holding))
                {
                    this.settleActionButton.SetActive(true);
                }

                if (!Oberkommando.SAVE.AllCivilizations[0].OwnsHolding(holding) && Oberkommando.SAVE.AllCivilizations[0].HasPoliticalPower())
                {
                    this.claimActionButton.SetActive(true);
                }
            }

            if (unit.Population.Pops.Count >= 1)
            {
                for (int i = 0; i < this.popSlotViews.Count; i++)
                {
                    if (i < unit.Population.Pops.Count)
                    {
                        this.popSlotViews[i].Couple(unit.Population.Pops[i]);
                        this.popSlotViews[i].Refresh();
                        this.popSlotViews[i].Display(true);
                    }
                    else
                    {
                        this.popSlotViews[i].Display(false);
                    }
                }
            }
            else
            {
                foreach (PopSlotView psv in this.popSlotViews)
                {
                    psv.Display(false);
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
