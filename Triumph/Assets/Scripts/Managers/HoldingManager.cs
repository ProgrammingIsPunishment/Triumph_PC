using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingManager : MonoBehaviour
{
    public GameObject terrain;
    public GameObject FogOfWar;
    public GameObject unit;
    public Holding holding;

    public void UpdateFromHolding(Holding holding)
    {
        holding.HoldingManager = this;
        this.holding = holding;
    }

    public void ShowDiscovered()
    {
        this.terrain.SetActive(true);
        this.FogOfWar.SetActive(false);
    }

    public void OnClickEvent()
    {
        Oberkommando.UI_CONTROLLER.holdingdetailsManager.UpdateDisplay(this.holding);
        Oberkommando.UI_CONTROLLER.holdingdetailsManager.Show();
    }
}
