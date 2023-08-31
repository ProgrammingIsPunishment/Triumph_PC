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
        Oberkommando.SAVE_CONTROLLER = this.gameObject.GetComponent<SaveController>();
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
        testingSave = Oberkommando.SAVE_CONTROLLER.NewGame("onyx");
        List<Civilization> tempCivilizations = new List<Civilization>
        {
            new Civilization("America", new List<string>(), new List<string>(), new List<string>())
        };
        testingSave.AllCivilizations = tempCivilizations;
        testingSave.AllCivilizations[0].DiscoveredHoldingGUIDs.Add(testingSave.AllHoldings.Find(h => h.DisplayName == "greenwood").GUID);
        testingSave.AllCivilizations[0].InfluentialPeopleGUIDs.Add("abraham");
        testingSave.AllCivilizations[0].LeaderGUIDs.Add("abraham");
        List<Unit> tempUnits = new List<Unit>
        {
            new Unit(Guid.NewGuid().ToString().ToLower(),"Abraham",UnitType.Leader,"abraham",testingSave.AllHoldings.Find(h => h.DisplayName == "greenwood").GUID)
        };
        testingSave.AllUnits = tempUnits;
        foreach (Unit u in testingSave.AllUnits)
        {
            testingSave.AllHoldings.Find(h=>h.GUID == u.HoldingGUID).Unit = u;
        }
        Oberkommando.SAVE_CONTROLLER.Save(testingSave);


        //LOADING SAVE GAME
        //testingSave = Oberkommando.SAVE_CONTROLLER.Load("New Game");

        Oberkommando.SAVE = testingSave;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.AssignControllers();
        this.ForTesting();
        this.InitializeHoldings(Oberkommando.SAVE.AllHoldings);
        this.ShowDiscoveredHoldings(Oberkommando.SAVE, Oberkommando.SAVE.AllCivilizations[0]);
    }
}
