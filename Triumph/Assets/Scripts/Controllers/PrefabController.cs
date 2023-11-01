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
        GameObject tempHolding = Instantiate(Resources.Load<GameObject>("Prefabs/Holding"), new Vector3((holding.XPosition * 10), 0f, (holding.ZPosition * 10)), Quaternion.identity);
        tempHolding.GetComponent<HoldingManager>().UpdateFromHolding(holding);
        tempHolding.transform.SetParent(Oberkommando.GAME_CONTROLLER.Gridmap.transform);

        tempHolding.SetActive(false);
    }

    public void InstantiateTerrainModel(Holding holding)
    {
        GameObject tempTerrain = Instantiate(Resources.Load<GameObject>($"Models/Terrain/{holding.TerrainType.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempTerrain.transform.SetParent(holding.HoldingManager.gameObject.transform);
        tempTerrain.transform.localPosition = new Vector3(0f, 0f, 0f);
        Destroy(holding.HoldingManager.terrain);
        holding.HoldingManager.terrain = tempTerrain;

        tempTerrain.SetActive(false);
    }

    public void InstantiateResourrceModel(Holding holding, ResourceItem resourceItem)
    {
        GameObject tempResource = Instantiate(Resources.Load<GameObject>($"Models/Resources/{resourceItem.GUID.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempResource.transform.SetParent(holding.HoldingManager.gameObject.transform);
        tempResource.transform.localPosition = new Vector3(0f, 1.5f, 0f);
        holding.HoldingManager.resourceObject = tempResource;

        tempResource.SetActive(false);
    }

    public void InstantiateUnitModel(Holding holding)
    {
        GameObject tempUnit = Instantiate(Resources.Load<GameObject>($"Models/Units/{holding.Unit.UnitType.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempUnit.transform.SetParent(holding.HoldingManager.gameObject.transform);
        tempUnit.transform.localPosition = new Vector3(0f, 0f, 0f);
        Destroy(holding.HoldingManager.unitObject);
        holding.HoldingManager.unitObject = tempUnit;

        tempUnit.SetActive(true);
    }
}