using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class Holding
{
    [SerializeField] public string GUID { get; set; }
    [SerializeField] public string Name { get; set; }
    [SerializeField] public int XPosition { get; set; }
    [SerializeField] public int ZPosition { get; set; }
    [SerializeField] public TerrainType TerrainType { get; set; }
    [SerializeField] public Civilization Owner { get; set; }
    [SerializeField] public List<Dispatch> Dispatches { get; set; }
    [SerializeField] public List<Holding> AdjacentHoldings { get; set; }

    [NonSerialized] public HoldingManager CoupledHoldingDisplay = null;

    public Holding(string guid, string name, int xPosition, int zPosition, TerrainType terrainType)
    {
        this.GUID = guid;
        this.Name = name;
        this.XPosition = xPosition;
        this.ZPosition = zPosition;
        this.TerrainType = terrainType;
        this.AdjacentHoldings = new List<Holding>();
        this.Dispatches = new List<Dispatch>();
    }

    public void UpdateVisibility()
    {
        this.CoupledHoldingDisplay.ShowDiscovered();
    }

    public void TakeAction()
    {
        List<Dispatch> uncompletedDisbatches = this.Dispatches.Where(d => !d.IsCompleted).ToList();

        if (uncompletedDisbatches.Count >= 1)
        {
            Dispatch mostRecentDispatch = uncompletedDisbatches[0];

            Task topTask = mostRecentDispatch.Tasks[0];

            switch (topTask.TaskType)
            {
                case TaskType.BuildUnit:
                    if (this.Owner.Coins >= 10)
                    {
                        this.Owner.Coins -= 10;
                        List<Unit> units = Oberkommando.SAVE.Units.Where(u => u.Owner.GUID == this.Owner.GUID && u.UnitTemplate.GUID != "leadertemplate").OrderByDescending(u => u.Name).ToList();
                        int unitInteration = int.Parse(units[0].Name.Split(" ")[0]) + 1;
                        string nameConvention = units[0].Name.Split(" ")[1];
                        Unit newUnit = new Unit($"{nameConvention}{unitInteration}", $"{unitInteration} {nameConvention}", this.XPosition, this.ZPosition);
                        newUnit.UnitTemplate = units[0].UnitTemplate;
                        newUnit.Owner = this.Owner;
                        Oberkommando.SAVE.Units.Add(newUnit);
                        Oberkommando.SYNTAXLIBRARY.AddUnit(newUnit);
                        Oberkommando.PREFAB_SERVICE.InstantiateUnitModel(newUnit, Oberkommando.UI_CONTROLLER.Gridmap);
                        mostRecentDispatch.Completed();
                    }
                    break;
            }
        }
    }

    public bool IsOwner(Civilization civilization)
    {
        bool result = false;

        if (this.Owner != null) 
        {
            if (this.Owner.GUID == civilization.GUID)
            {
                result = true;
            }
        }

        return result;
    }
}
