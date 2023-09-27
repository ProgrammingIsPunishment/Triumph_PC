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

    public void InstantiateUnitModel(Holding holding)
    {
        GameObject tempUnit = Instantiate(Resources.Load<GameObject>($"Models/Units/{holding.Unit.UnitType.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempUnit.transform.SetParent(holding.HoldingManager.gameObject.transform);
        tempUnit.transform.localPosition = new Vector3(0f, 0f, 0f);
        Destroy(holding.HoldingManager.unit);
        holding.HoldingManager.unit = tempUnit;

        tempUnit.SetActive(true);
    }
}