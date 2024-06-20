using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class PrefabsService : MonoBehaviour
{
    public void InstantiateHoldingModel(Holding holding, GameObject gridMap)
    {
        GameObject tempHoldingObject = Instantiate(Resources.Load<GameObject>("Prefabs/Holding"), new Vector3((holding.XPosition * 10), 0f, (holding.ZPosition * 10)), Quaternion.identity);
        tempHoldingObject.GetComponent<HoldingManager>().Couple(holding);
        tempHoldingObject.transform.SetParent(gridMap.transform);
        tempHoldingObject.SetActive(true);
        //holding.HoldingDisplayManager.Initialize();
        //holding.HoldingDisplayManager.Show(false);
        ////tempHoldingObject.(false);
    }

    public void InstantiateTerrainModel(Holding holding)
    {
        GameObject tempTerrainObject = Instantiate(Resources.Load<GameObject>($"Models/Terrain/{holding.TerrainType.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempTerrainObject.transform.SetParent(holding.CoupledHoldingDisplay.transform);
        tempTerrainObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        holding.CoupledHoldingDisplay.terrainObject = tempTerrainObject;

        tempTerrainObject.SetActive(true);
    }

    public void InstantiateUnitModel(Unit unit, GameObject gridMap)
    {
        GameObject tempUnitObject = Instantiate(Resources.Load<GameObject>("Prefabs/Unit"), new Vector3((unit.XPosition * 10), 0f, (unit.ZPosition * 10)), Quaternion.identity);
        tempUnitObject.GetComponent<UnitManager>().Couple(unit);
        tempUnitObject.transform.SetParent(gridMap.transform);
        GameObject tempModelObject = Instantiate(Resources.Load<GameObject>($"Models/Units/{unit.UnitTemplate.ModelName}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
        tempModelObject.transform.SetParent(tempUnitObject.transform);
        tempModelObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        tempUnitObject.GetComponent<UnitManager>().AssignModel(tempModelObject);

        tempUnitObject.SetActive(true);
        //GameObject tempUnitObject = Instantiate(Resources.Load<GameObject>("Prefabs/Unit"), new Vector3((unit.XPosition * 10), 0f, (unit.ZPosition * 10)), Quaternion.identity);
        //unit.UnitDisplayManager = tempUnitObject.GetComponent<UnitDisplayManager>();
        //tempUnitObject.transform.SetParent(Oberkommando.GAME_CONTROLLER.Gridmap.transform);

        //unit.UnitDisplayManager.Show(true);
        ////tempHoldingObject.(false);
    }

    public void InstantiateDispatchListItem(Dispatch dispatch, GameObject scrollViewContent)
    {
        GameObject tempListItemObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DispatchListItem"), new Vector3(0f,0f,0f), Quaternion.identity);
        tempListItemObject.GetComponent<DispatchListItemManager>().Refresh(dispatch);
        tempListItemObject.transform.SetParent(scrollViewContent.transform);
        tempListItemObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    //public void InstantiateResourceModel(Holding holding, ResourceItem resourceItem)
    //{
    //    GameObject tempResourceObject = Instantiate(Resources.Load<GameObject>($"Models/Resources/{resourceItem.GUID.ToString()}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
    //    tempResourceObject.transform.SetParent(holding.HoldingDisplayManager.gameObject.transform);
    //    tempResourceObject.transform.localPosition = new Vector3(0f, 1.5f, 0f);
    //    holding.HoldingDisplayManager.resourceObject = tempResourceObject;

    //    tempResourceObject.SetActive(true);
    //}

    //public void InstantiateBuildingModel(Holding holding, Building building)
    //{
    //    List<Vector3> tempLotVectors = new List<Vector3>() { 
    //        new Vector3(-2.5f, -2.2f, -2.5f),
    //        new Vector3(-2.5f, -2.2f, 2.5f),
    //        new Vector3(2.5f, -2.2f, -2.5f),
    //        new Vector3(2.5f, -2.2f, 2.5f)
    //    };

    //    string workingBuildingFileName = building.ModelFileName;

    //    if (!building.Construction.IsCompleted)
    //    {
    //        workingBuildingFileName = "underconstruction";
    //    }

    //    GameObject tempBuildingObject = Instantiate(Resources.Load<GameObject>($"Models/Buildings/{workingBuildingFileName}"), new Vector3(0f, 0f, 0f), Quaternion.identity);
    //    tempBuildingObject.transform.SetParent(holding.HoldingDisplayManager.gameObject.transform);
    //    tempBuildingObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    //    tempBuildingObject.transform.localPosition = tempLotVectors[building.Lot-1];
    //    holding.HoldingDisplayManager.UpdateBuildingModel(building.Lot, tempBuildingObject);

    //    tempBuildingObject.SetActive(true);
    //}

    //public void InstantiateUnitPrefab(Unit unit)
    //{
    //    GameObject tempUnitObject = Instantiate(Resources.Load<GameObject>("Prefabs/Unit"), new Vector3((unit.XPosition * 10), 0f, (unit.ZPosition * 10)), Quaternion.identity);
    //    unit.UnitDisplayManager = tempUnitObject.GetComponent<UnitDisplayManager>();
    //    tempUnitObject.transform.SetParent(Oberkommando.GAME_CONTROLLER.Gridmap.transform);

    //    unit.UnitDisplayManager.Show(true);
    //    //tempHoldingObject.(false);
    //}
}