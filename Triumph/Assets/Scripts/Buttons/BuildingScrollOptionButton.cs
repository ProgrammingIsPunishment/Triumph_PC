using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BuildingScrollOptionButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI buildingNameText;
    [NonSerialized] public Building CoupledBuilding = null;

    //public event EventHandler OnBuildingScrollOptionButtonClick;

    public void Initialize()
    {
        //Oberkommando.UI_CONTROLLER.OnNewHoldingClickForSelection += EventHandler_NewHoldingClickForSelection;
    }

    public void Couple(Building building)
    {
        this.CoupledBuilding = building;
        this.buildingNameText.text = building.DisplayName;
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
}
