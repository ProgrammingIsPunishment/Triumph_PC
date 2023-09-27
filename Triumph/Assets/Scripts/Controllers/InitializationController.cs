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

    private void InitializeHoldings(List<Holding> allHoldings)
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
        Oberkommando.UI_CONTROLLER.HideAll();
        //this.ForTesting();
        this.InitializeHoldings(Oberkommando.SAVE.AllHoldings);
        //Oberkommando.CAMERA_MANAGER.CenterCameraOnHolding();
        Oberkommando.UI_CONTROLLER.SetUIState(UIState.Default);
        Oberkommando.TURN_CONTROLLER.StartTurn(Oberkommando.SAVE.AllCivilizations[0]);
    }
}
