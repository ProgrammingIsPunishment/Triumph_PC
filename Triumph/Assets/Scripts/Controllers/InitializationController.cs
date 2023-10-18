using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationController : MonoBehaviour
{
    [SerializeField] private GameObject holdingDetailsUIGameObject;

    private void AssignControllers()
    {
        Oberkommando.INITIALIZATION_CONTROLLER = this.gameObject.GetComponent<InitializationController>();
        Oberkommando.PREFAB_CONTROLLER = this.gameObject.GetComponent<PrefabController>();
        Oberkommando.GAME_CONTROLLER = this.gameObject.GetComponent<GameController>();
        Oberkommando.UI_CONTROLLER = this.gameObject.GetComponent<UIController>();
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.AssignControllers();
        this.InitializeControllers();
        Oberkommando.UI_CONTROLLER.HideAll();
        this.InitializeModels(Oberkommando.SAVE.AllHoldings);
        //Oberkommando.CAMERA_MANAGER.CenterCameraOnHolding();
        Oberkommando.UI_CONTROLLER.NewUIState(UIState.Default,null);
        Oberkommando.TURN_CONTROLLER.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }
}
