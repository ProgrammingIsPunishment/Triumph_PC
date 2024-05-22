using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit
{
    [SerializeField] public string GUID { get; set; }
    [SerializeField] public string Name { get; set; }
    [SerializeField] public int XPosition { get; set; }
    [SerializeField] public int ZPosition { get; set; }
    [SerializeField] public string ModelName { get; set; }
    [SerializeField] public List<Dispatch> Dispatches { get; set; }

    [NonSerialized] public UnitManager CoupledUnitManager = null;

    public Unit(string guid, string name, int xPosition, int zPosition, string modelName)
    {
        this.GUID = guid;
        this.Name = name;
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.ModelName = modelName;
        this.Dispatches = new List<Dispatch>();
    }

    public void TakeAction()
    {
        List<Dispatch> uncompletedDisbatches = this.Dispatches.Where(d=>!d.IsCompleted).ToList();

        foreach (Dispatch d in uncompletedDisbatches)
        {
            
        }
    }
}
