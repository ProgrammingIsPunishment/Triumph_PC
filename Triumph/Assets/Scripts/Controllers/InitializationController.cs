using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationController : MonoBehaviour
{
    [SerializeField] private GameObject holdingDetailsUIGameObject;
    [SerializeField] private GameObject naturalResourcesTabUIGameObject;
    [SerializeField] private GameObject unitTabUIGameObject;
    [SerializeField] private GameObject summaryTabUIGameObject;

    private void AssignControllers()
    {
        Oberkommando.INITIALIZATION_CONTROLLER = this.gameObject.GetComponent<InitializationController>();
        Oberkommando.PREFAB_CONTROLLER = this.gameObject.GetComponent<PrefabController>();
        Oberkommando.GAME_CONTROLLER = this.gameObject.GetComponent<GameController>();
        Oberkommando.UI_CONTROLLER = this.gameObject.GetComponent<UIController>();
    }

    private void InitializeManagers()
    {
        Oberkommando.UI_CONTROLLER.holdingDetailsView.summaryTabManager.Initialize();
        Oberkommando.UI_CONTROLLER.holdingDetailsView.unitTabManager.Initialize();
        Oberkommando.UI_CONTROLLER.holdingDetailsView.naturalResourcesTabManager.Initialize();
    }

    private void InitializeHoldingModels(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            Oberkommando.PREFAB_CONTROLLER.InstantiateHoldingPrefab(h);
            Oberkommando.PREFAB_CONTROLLER.InstantiateTerrainModel(h);
            //if (h.Unit != null)
            //{
            //    Oberkommando.PREFAB_CONTROLLER.InstantiateUnitModel(h);
            //}
            foreach (ResourceItem ri in h.NaturalResourcesInventory.ResourceItems)
            {
                if (ri.ModelFileName != "")
                {
                    Oberkommando.PREFAB_CONTROLLER.InstantiateResourceModel(h, ri);
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
        this.InitializeManagers();
        Oberkommando.UI_CONTROLLER.HideAll();
        this.InitializeHoldingModels(Oberkommando.SAVE.AllHoldings);
        this.InitializeUnitModels(Oberkommando.SAVE.AllUnits);
        //Oberkommando.CAMERA_MANAGER.CenterCameraOnHolding();
        //Oberkommando.UI_CONTROLLER.RefreshToDefault();
        Oberkommando.UI_CONTROLLER.NewUIState(UIState.HoldingDetails);
        //Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(Oberkommando.SAVE.AllCivilizations[0]);
        Oberkommando.TURN_CONTROLLER.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }
}
