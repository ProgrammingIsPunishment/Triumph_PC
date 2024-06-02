using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour
{
    [SerializeField] public bool IsDebugMode;
    [SerializeField] public bool ShowChokePoints;

    private void Start()
    {
        Oberkommando.DEBUG = this;
    }

    public bool ShowCokePoint(Holding holding)
    {
        HoldingValueSet holdingValueSet = Oberkommando.VALUELIBRARY.HoldingValueSets.Find(hvs=>hvs.HoldingGUD == holding.GUID);
        return holdingValueSet.IsChokePoint;
    }
}
