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
    }

    private void InitializeViews()
    {
        Oberkommando.UI_CONTROLLER.holdingView.summaryTabManager.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.unitSupplyTab.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.naturalResourcesTab.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.storageTab.Initialize();
        Oberkommando.UI_CONTROLLER.holdingView.improvementsTab.Initialize();
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

    // Start is called before the first frame update
    void Start()
    {
        //Order very important
        this.AssignControllers();
        this.InitializeViews();
        this.InitializeHoldingModels(Oberkommando.SAVE.AllHoldings);
        this.InitializeUnitModels(Oberkommando.SAVE.AllUnits);
        //Oberkommando.CAMERA_MANAGER.CenterCameraOnHolding();
        //Oberkommando.UI_CONTROLLER.RefreshToDefault();
        Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.Initialize);
        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(Oberkommando.SAVE.AllCivilizations[0]);
        Oberkommando.TURN_CONTROLLER.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }
}
