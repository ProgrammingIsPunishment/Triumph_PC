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

    private void ShowDiscoveredHoldings(Save save, Civilization civilization)
    {
        foreach (string s in civilization.DiscoveredHoldingGUIDs)
        {
            save.AllHoldings.Find(h => h.GUID == s).HoldingManager.ShowDiscovered();
        }
    }

    private void ForTesting()
    {
        Save testingSave = null;

        ////NEW GAME
        //List<Civilization> tempCivilizations = new List<Civilization>();
        //tempCivilizations.Add(new Civilization("America",new List<string>()));
        //testingSave = Oberkommando.SAVE_CONTROLLER.NewGame("onyx",tempCivilizations);
        //Oberkommando.SAVE_CONTROLLER.Save(testingSave);

        //LOADING SAVE GAME
        testingSave = Oberkommando.SAVE_CONTROLLER.Load("New Game");
        testingSave.AllCivilizations[0].DiscoveredHoldingGUIDs.Add("047ed24a-ba4e-47a1-9be2-d8fed3c41fb0");

        Oberkommando.SAVE = testingSave;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.AssignControllers();
        this.ForTesting();
        this.GenerateHoldingModels(Oberkommando.SAVE.AllHoldings);
        this.ShowDiscoveredHoldings(Oberkommando.SAVE, Oberkommando.SAVE.AllCivilizations[0]);
    }
}
