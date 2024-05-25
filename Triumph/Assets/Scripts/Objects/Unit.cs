using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit
{
    [SerializeField] public string GUID { get; set; }
    [SerializeField] public string Name { get; set; }
    [SerializeField] public int XPosition { get; set; }
    [SerializeField] public int ZPosition { get; set; }
    [SerializeField] public string ModelName { get; set; }
    [SerializeField] public Civilization Owner { get; set; }
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

        if (uncompletedDisbatches.Count >= 1)
        {
            Dispatch mostRecentDispatch = uncompletedDisbatches[0];

            Task topTask = mostRecentDispatch.Tasks[0];

            switch (topTask.TaskType)
            {
                case TaskType.Move:
                    Holding destinationHolding = Oberkommando.UTILITIES_SERVICE.GetHoldingByGUID(topTask.Parameter);
                    this.Move(destinationHolding);
                    this.Claim(destinationHolding);
                    mostRecentDispatch.Completed();
                    break;
            }
        }
    }

    private void Move(Holding holding)
    {
        this.XPosition = holding.XPosition;
        this.ZPosition = holding.ZPosition;

        this.CoupledUnitManager.transform.localPosition = new Vector3(this.XPosition*10,0f,this.ZPosition*10);
    }

    private void Claim(Holding holding)
    {
        holding.Owner = this.Owner;
    }
}
