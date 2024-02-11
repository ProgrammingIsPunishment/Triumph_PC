using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PrefabController : MonoBehaviour
{
    public void InstantiateHoldingPrefab(Holding holding)
    {
        GameObject tempHoldingObject = Instantiate(Resources.Load<GameObject>("Prefabs/Holding"), new Vector3((holding.XPosition * 10), 0f, (holding.ZPosition * 10)), Quaternion.identity);
        tempHoldingObject.GetComponent<HoldingDisplayManager>().Couple(holding);
        tempHoldingObject.transform.SetParent(Oberkommando.GAME_CONTROLLER.Gridmap.transform);

        holding.HoldingDisplayManager.Initialize();
        holding.HoldingDisplayManager.Show(false);
        //tempHoldingObject.(false);
    }

    public void InstantiateTerrainModel(Holding holding)
    {
        GameObject tempTerrainObject = Instantiate(Resources.Load<GameObject>($"Models/Terrain/{holding.TerrainType.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempTerrainObject.transform.SetParent(holding.HoldingDisplayManager.gameObject.transform);
        tempTerrainObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        //Destroy(holding.HoldingManager.terrain);
        holding.HoldingDisplayManager.terrainObject = tempTerrainObject;

        tempTerrainObject.SetActive(true);
        //tempTerrainObject.SetActive(false);
    }

    public void InstantiateResourceModel(Holding holding, ResourceItem resourceItem)
    {
        GameObject tempResourceObject = Instantiate(Resources.Load<GameObject>($"Models/Resources/{resourceItem.GUID.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempResourceObject.transform.SetParent(holding.HoldingDisplayManager.gameObject.transform);
        tempResourceObject.transform.localPosition = new Vector3(0f, 1.5f, 0f);
        holding.HoldingDisplayManager.resourceObject = tempResourceObject;

        tempResourceObject.SetActive(true);
    }

    public void InstantiateBuildingModel(Holding holding, Building building)
    {
        List<Vector3> tempLotVectors = new List<Vector3>() { 
            new Vector3(-2.5f, -2.2f, -2.5f),
            new Vector3(-2.5f, -2.2f, 2.5f),
            new Vector3(2.5f, -2.2f, -2.5f),
            new Vector3(2.5f, -2.2f, 2.5f)
        };

        string workingBuildingFileName = building.ModelFileName;

        if (!building.Construction.IsCompleted)
        {
            workingBuildingFileName = "underconstruction";
        }

        GameObject tempBuildingObject = Instantiate(Resources.Load<GameObject>($"Models/Buildings/{workingBuildingFileName}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempBuildingObject.transform.SetParent(holding.HoldingDisplayManager.gameObject.transform);
        tempBuildingObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        tempBuildingObject.transform.localPosition = tempLotVectors[building.Lot-1];
        holding.HoldingDisplayManager.UpdateBuildingModel(building.Lot, tempBuildingObject);

        tempBuildingObject.SetActive(true);
    }

    public void InstantiateUnitPrefab(Unit unit)
    {
        GameObject tempUnitObject = Instantiate(Resources.Load<GameObject>("Prefabs/Unit"), new Vector3((unit.XPosition * 10), 0f, (unit.ZPosition * 10)), Quaternion.identity);
        unit.UnitDisplayManager = tempUnitObject.GetComponent<UnitDisplayManager>();
        tempUnitObject.transform.SetParent(Oberkommando.GAME_CONTROLLER.Gridmap.transform);

        unit.UnitDisplayManager.Show(true);
        //tempHoldingObject.(false);
    }
}