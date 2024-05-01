using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private Unit CoupledUnit = null;

    public void OnClickEvent()
    {
        
    }

    public void Couple(Unit unit)
    {
        this.CoupledUnit = unit;
        this.CoupledUnit.CoupledUnitManager = this;
    }
}
