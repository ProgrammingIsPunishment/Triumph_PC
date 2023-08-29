using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingManager : MonoBehaviour
{
    [SerializeField] public GameObject terrain;

    public void UpdateFromHolding(Holding holding)
    {
        holding.HoldingManager = this;
    }
}
