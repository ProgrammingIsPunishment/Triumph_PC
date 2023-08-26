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
        Debug.Log("HELLO");
        GameObject tempHolding = Instantiate(Resources.Load<GameObject>("Prefabs/Holding"), new Vector3((holding.XPosition * 10), 0f, (holding.ZPosition * 10)), Quaternion.identity);
        tempHolding.GetComponent<HoldingManager>().UpdateFromHolding(holding);
        tempHolding.transform.SetParent(Oberkommando.GAME_CONTROLLER.Gridmap.transform);
        //this.InstantiateTerrainPrefab(holding.HoldingManager);

        tempHolding.SetActive(true);
    }

    //private void InstantiateTerrainPrefab(HoldingManager holdingManager)
    //{
    //    GameObject tempTerrain = Instantiate(Resources.Load<GameObject>($"Voxel Models/Terrain/{holdingManager.Holding.Terrain.DisplayOption.VoxelModelName}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
    //    tempTerrain.transform.SetParent(holdingManager.gameObject.transform);
    //    tempTerrain.transform.localPosition = new Vector3(0f, 0f, 0f);
    //    holdingManager.SetTerrainObject(tempTerrain);

    //    tempTerrain.SetActive(true);
    //}

    //public void InstantiateHoldingResourcePrefab(GameObject parent, AssemblyComponent component)
    //{
    //    GameObject tempResourceObject = Instantiate(Resources.Load<GameObject>($"Voxel Models/Natural Resources/{component.AssemblyComponentBase.DisplayOption.PrefabName}"));
    //    tempResourceObject.transform.SetParent(parent.transform);
    //    Vector3 tempLocalPosition = tempResourceObject.transform.localPosition;
    //    tempResourceObject.transform.localPosition = new Vector3(0f, 1.5f, 0f);

    //    tempResourceObject.SetActive(true);
    //}

    //public void InstantiateHoldingUnitVoxelModel(GameObject parent, Unit unit)
    //{
    //    GameObject tempUnitObject = Instantiate(Resources.Load<GameObject>($"Voxel Models/Units/{unit.DisplayOption.PrefabName}"));
    //    tempUnitObject.transform.SetParent(parent.transform);
    //    tempUnitObject.transform.localPosition = new Vector3(0f, 0f, 0f);
    //    parent.GetComponent<HoldingManager>().unitObject = tempUnitObject;

    //    tempUnitObject.SetActive(true);
    //}
}