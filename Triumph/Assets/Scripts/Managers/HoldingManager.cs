using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingManager : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;

    public void UpdateFromHolding(Holding holding)
    {
        holding.HoldingManager = this;
    }
}
