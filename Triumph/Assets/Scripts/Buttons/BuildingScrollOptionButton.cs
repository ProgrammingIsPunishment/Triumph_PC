using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScrollOptionButton : MonoBehaviour
{
    private Building CoupledBuilding = null;

    public void Couple(Building building)
    {
        this.CoupledBuilding = building;
    }

    public void Display(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void OnClickEvent()
    {
        Debug.Log(this.CoupledBuilding.DisplayName);
    }
}
