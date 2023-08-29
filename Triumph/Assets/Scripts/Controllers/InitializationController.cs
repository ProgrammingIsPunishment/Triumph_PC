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
        Oberkommando.SAVE_CONTROLLER = this.gameObject.GetComponent<SaveController>();
    }

    private void GenerateHoldingModels(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            Oberkommando.PREFAB_CONTROLLER.InstantiateHoldingPrefab(h);
            Oberkommando.PREFAB_CONTROLLER.InstantiateTerrainModel(h);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.AssignControllers();
        Save newSave = Oberkommando.SAVE_CONTROLLER.NewGame();
        this.GenerateHoldingModels(newSave.AllHoldings);
    }
}
