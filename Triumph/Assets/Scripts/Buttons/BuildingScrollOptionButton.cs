using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class BuildingScrollOptionButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI buildingNameText;
    [NonSerialized] public Building CoupledBuilding = null;

    private List<ResourceRequirementView> resourceRequirementViews = new List<ResourceRequirementView>();

    //public event EventHandler OnBuildingScrollOptionButtonClick;

    public void Couple(Building building)
    {
        this.CoupledBuilding = building;
        this.buildingNameText.text = building.DisplayName;

        foreach (ResourceRequirementView rrv in this.resourceRequirementViews)
        {
            rrv.Display(false);
        }

        for (int i = 0; i < building.Construction.RequiredResources.Count; i++)
        {
            Tuple<ResourceItem, int> tempRequirement = building.Construction.RequiredResources[i];
            this.resourceRequirementViews[i].Refresh(tempRequirement.Item1, tempRequirement.Item2);
            this.resourceRequirementViews[i].Display(true);
        }
    }

    public void Display(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.buildingSelectionView.Display(false);
        Building workingBuilding = this.CoupledBuilding.CreateInstance(Oberkommando.SELECTED_LOT);

        Oberkommando.SELECTED_HOLDING.BuildBuilding(workingBuilding);
        Oberkommando.PREFAB_CONTROLLER.InstantiateBuildingModel(Oberkommando.SELECTED_HOLDING, workingBuilding);
        Oberkommando.SELECTED_UNIT.Build(workingBuilding.Construction.RequiredComponents);
        Oberkommando.UI_CONTROLLER.holdingView.improvementsTab.ShowImprovableLots(false);
        Oberkommando.UI_CONTROLLER.holdingView.Refresh(Oberkommando.SELECTED_HOLDING,Oberkommando.SELECTED_UNIT);
    }

    public void Initialize()
    {
        this.resourceRequirementViews = this.GetComponentsInChildren<ResourceRequirementView>().ToList();
    }
}
