using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationController : MonoBehaviour
{
    [SerializeField] private GameObject holdingDetailsUIGameObject;
    [SerializeField] private GameObject naturalResourcesTabUIGameObject;

    private void AssignControllers()
    {
        Oberkommando.INITIALIZATION_CONTROLLER = this.gameObject.GetComponent<InitializationController>();
        Oberkommando.PREFAB_CONTROLLER = this.gameObject.GetComponent<PrefabController>();
        Oberkommando.GAME_CONTROLLER = this.gameObject.GetComponent<GameController>();
        Oberkommando.UI_CONTROLLER = this.gameObject.GetComponent<UIController>();
    }

    private void InitializeManagers()
    {
        NaturalResourcesTabManager tempNaturalResourcesTabManager = this.naturalResourcesTabUIGameObject.GetComponent<NaturalResourcesTabManager>();
        tempNaturalResourcesTabManager.Initialize();
        this.holdingDetailsUIGameObject.GetComponent<HoldingDetailsManager>().AssignManagers(tempNaturalResourcesTabManager);
    }

    private void InitializeControllers()
    {
        Oberkommando.UI_CONTROLLER.HoldingDetailsUIProcess = new HoldingDetailsUIProcess(this.holdingDetailsUIGameObject.GetComponent<HoldingDetailsManager>());
        Oberkommando.UI_CONTROLLER.MoveLeaderUnitUIProcess = new MoveLeaderUnitUIProcess();
    }

    private void InitializeModels(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            Oberkommando.PREFAB_CONTROLLER.InstantiateHoldingPrefab(h);
            Oberkommando.PREFAB_CONTROLLER.InstantiateTerrainModel(h);
            if (h.Unit != null)
            {
                Oberkommando.PREFAB_CONTROLLER.InstantiateUnitModel(h);
            }
            if (h.NaturalResourcesInventory.ResourceItems.Count >= 1)
            {
                foreach (ResourceItem ri in h.NaturalResourcesInventory.ResourceItems)
                {
                    switch (ri.GUID.ToLower())
                    {
                        case "forest": Oberkommando.PREFAB_CONTROLLER.InstantiateResourrceModel(h,ri); break;
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Order very important
        this.AssignControllers();
        this.InitializeManagers();
        this.InitializeControllers();
        Oberkommando.UI_CONTROLLER.HideAll();
        this.InitializeModels(Oberkommando.SAVE.AllHoldings);
        //Oberkommando.CAMERA_MANAGER.CenterCameraOnHolding();
        Oberkommando.UI_CONTROLLER.RefreshToDefault();
        Oberkommando.UI_CONTROLLER.ShowDiscoveredHoldings(Oberkommando.SAVE.AllCivilizations[0]);
        Oberkommando.UI_CONTROLLER.ShowExplorableHoldings(Oberkommando.SAVE.AllCivilizations[0]);
        Oberkommando.TURN_CONTROLLER.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }
}
