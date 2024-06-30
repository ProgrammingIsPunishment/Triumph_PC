using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit
{
    public string GUID { get; set; }
    public string Name { get; set; }
    public int XPosition { get; set; }
    public int ZPosition { get; set; }
    public UnitTemplate UnitTemplate { get; set; }
    public Civilization Owner { get; set; }
    public UnitAI UnitAI { get; set; }
    public Dispatch Dispatch { get; set; }

    public UnitManager CoupledUnitManager = null;

    public Unit(string guid, string name, int xPosition, int zPosition)
    {
        this.GUID = guid;
        this.Name = name;
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.UnitAI = new UnitAI(this);
        //this.Dispatch = new List<Dispatch>();
    }

    public void ProcessDispatch(Dispatch dispatch)
    {
        this.Dispatch = dispatch;
        foreach (Task t in dispatch.Tasks)
        {
            switch (t.TaskType)
            {
                case TaskType.Move:
                    Holding holdingAtPositon = Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(this.XPosition,this.ZPosition);
                    Holding destinationHolding = Oberkommando.UTILITIES_SERVICE.GetHoldingByGUID(dispatch.Tasks[0].Parameter);
                    this.UnitAI.DeterminePath(holdingAtPositon, destinationHolding);
                    break;
                default:
                    //Should never be hit
                    break;
            }
        }
    }

    //public void TakeAction()
    //{
    //    if (this.Dispatch != null)
    //    {
    //        switch (this.Dispatch.Tasks[0].TaskType)
    //        {
    //            case TaskType.Move:
    //                Holding destinationHolding = Oberkommando.UTILITIES_SERVICE.GetHoldingByGUID(this.Dispatch.Tasks[0].Parameter);
    //                this.Move(destinationHolding);
    //                this.Claim(destinationHolding);
    //                this.Dispatch.Completed();
    //                break;
    //        }
    //    }

    //    //List<Dispatch> uncompletedDisbatches = this.Dispatch.Where(d=>!d.IsCompleted).ToList();

    //    //if (uncompletedDisbatches.Count >= 1)
    //    //{
    //    //    Dispatch mostRecentDispatch = uncompletedDisbatches[0];

    //    //    Task topTask = mostRecentDispatch.Tasks[0];

    //    //    switch (topTask.TaskType)
    //    //    {
    //    //        case TaskType.Move:
    //    //            Holding destinationHolding = Oberkommando.UTILITIES_SERVICE.GetHoldingByGUID(topTask.Parameter);
    //    //            this.Move(destinationHolding);
    //    //            this.Claim(destinationHolding);
    //    //            mostRecentDispatch.Completed();
    //    //            break;
    //    //    }
    //    //}
    //}

    //private void Move(Holding holding)
    //{
    //    this.XPosition = holding.XPosition;
    //    this.ZPosition = holding.ZPosition;

    //    this.CoupledUnitManager.transform.localPosition = new Vector3(this.XPosition*10,0f,this.ZPosition*10);
    //}

    //private void Claim(Holding holding)
    //{
    //    holding.Owner = this.Owner;
    //}
}
