using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour
{
    [SerializeField] public bool IsDebugMode;
    [SerializeField] public bool ShowChokePoints;
    [SerializeField] public bool ShowClusters;

    [NonSerialized] public List<Color> ClusterDebugColors = new List<Color>();

    private void Start()
    {
        Oberkommando.DEBUG = this;
    }

    public void GenerateClusterDebugColors(int count)
    {
        for (int i = 0; i < count; i++)
        {
            this.ClusterDebugColors.Add(Tools.RandomColor());
        }
    }
}
