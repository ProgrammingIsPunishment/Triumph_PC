using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingManager : MonoBehaviour
{
    public GameObject terrain;
    public GameObject FogOfWar;

    public void UpdateFromHolding(Holding holding)
    {
        holding.HoldingManager = this;
    }

    public void ShowDiscovered()
    {
        this.terrain.SetActive(true);
        this.FogOfWar.SetActive(false);
    }
}
