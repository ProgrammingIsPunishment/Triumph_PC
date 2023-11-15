using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject unexploredObject;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject selectableObject;

    [NonSerialized] public GameObject terrainObject = null;
    [NonSerialized] public GameObject resourceObject = null;

    public void Show(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void ShowExplored(bool isExplored)
    {
        if (isExplored)
        {
            //This holding has been explored
            this.unexploredObject.SetActive(false);
            this.terrainObject.SetActive(true);
            if (resourceObject != null)
            {
                this.resourceObject.SetActive(true);
            }
        }
        else
        {
            //This holding has - not - been explored
            this.unexploredObject.SetActive(true);
            this.terrainObject.SetActive(false);
            if (resourceObject != null)
            {
                this.resourceObject.SetActive(false);
            }
        }
    }

    public void ShowSelectable(bool isSelectable)
    {
        if (isSelectable) { this.selectableObject.SetActive(true); }
        else { this.selectableObject.SetActive(false); }
    }

    public void ShowSelected(bool isSelected)
    {
        if (isSelected) { this.selectedObject.SetActive(true); }
        else { this.selectedObject.SetActive(false); }
    }

    public void OnClickEvent()
    {

    }
}
