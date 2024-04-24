using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject unexploredObject;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject selectableObject;
    [SerializeField] private GameObject borderObject;

    [NonSerialized] public GameObject terrainObject = null;

    private Holding CoupledHolding = null;

    public void OnClickEvent()
    {

    }

    public void Couple(Holding holding) 
    {
        this.CoupledHolding = holding;
        this.CoupledHolding.CoupledHoldingDisplayManager = this;
    }
}