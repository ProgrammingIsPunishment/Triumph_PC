using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationController : MonoBehaviour
{
    private void AssignControllers()
    {
        Oberkommando.INITIALIZATION_CONTROLLER = this.gameObject.GetComponent<InitializationController>();
        Oberkommando.PREFAB_CONTROLLER = this.gameObject.GetComponent<PrefabController>();
        Oberkommando.GAME_CONTROLLER = this.gameObject.GetComponent<GameController>();
        Oberkommando.UI_CONTROLLER = this.gameObject.GetComponent<UIController>();
        Oberkommando.COLDSTORAGE_CONTROLLER = this.gameObject.GetComponent<ColdStorageController>();
    }

    public void InitializeControllers()
    {
        Oberkommando.COLDSTORAGE_CONTROLLER.Initialize();
        //Oberkommando.UI_CONTROLLER.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.unitView.Initialize();
    }

    private void InitializeViews()
    {
        Oberkommando.UI_CONTROLLER.holdingView.summaryTabManager.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.naturalResourcesTab.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.storageTab.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.improvementsTab.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.populationTab.Initialize();

        Oberkommando.UI_CONTROLLER.holdingView.unitView.unitInventoryView.Initialize();

        Oberkommando.UI_CONTROLLER.buildingSelectionView.Initialize();
    }

    private void DisplayDefaultViews()
    {
        Oberkommando.UI_CONTROLLER.holdingView.Hide();
        Oberkommando.UI_CONTROLLER.buildingSelectionView.Display(false);
    }

    private void InitializeHoldingModels(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            Oberkommando.PREFAB_CONTROLLER.InstantiateHoldingPrefab(h);
            Oberkommando.PREFAB_CONTROLLER.InstantiateTerrainModel(h);

            foreach (ResourceItem ri in h.NaturalResourcesInventory.ResourceItems)
            {
                if (ri.ModelFileName != "")
                {
                    Oberkommando.PREFAB_CONTROLLER.InstantiateResourceModel(h, ri);
                }
            }

            foreach (Building b in h.Buildings)
            {
                if (b.ModelFileName != "")
                {
                    Oberkommando.PREFAB_CONTROLLER.InstantiateBuildingModel(h, b);
                }
            }
        }
    }

    private void InitializeUnitModels(List<Unit> units)
    {
        foreach (Unit u in units)
        {
            Oberkommando.PREFAB_CONTROLLER.InstantiateUnitPrefab(u);
        }
    }

    private void InitializeEffects(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            h.PassEffectFromHolding();
            h.Population.DetermineTurnEffects();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Order very important
        this.AssignControllers();
        this.InitializeHoldingModels(Oberkommando.SAVE.AllHoldings);
        this.InitializeViews();
        this.InitializeControllers();
        this.DisplayDefaultViews();
        this.InitializeUnitModels(Oberkommando.SAVE.AllUnits);
        this.InitializeEffects(Oberkommando.SAVE.AllHoldings);
        //Oberkommando.CAMERA_MANAGER.CenterCameraOnHolding();
        //Oberkommando.UI_CONTROLLER.RefreshToDefault();
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.Initialize);
        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(Oberkommando.SAVE.AllCivilizations[0]);
        Oberkommando.TURN_CONTROLLER.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }
}
