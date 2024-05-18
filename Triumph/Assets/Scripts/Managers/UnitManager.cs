using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GameObject modelObject;

    private Unit CoupledUnit = null;

    public void OnClickEvent()
    {
        
    }

    public void Couple(Unit unit)
    {
        this.CoupledUnit = unit;
        this.CoupledUnit.CoupledUnitManager = this;
    }

    public void AssignModel(GameObject gameObject)
    {
        this.modelObject = gameObject;
    }
}
