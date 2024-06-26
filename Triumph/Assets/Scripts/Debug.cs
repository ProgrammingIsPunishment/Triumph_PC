using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour
{
    [SerializeField] public bool IsDebugMode;
    [SerializeField] public bool ShowChokePoints;
    [SerializeField] public bool ShowClusters;
    [SerializeField] public bool ShowWeights;

    private void Start()
    {
        Oberkommando.DEBUG = this;
    }
}
