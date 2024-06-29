using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ValueLibrary
{
    //public List<HoldingValueSet> HoldingValueSets { get; set; } = new List<HoldingValueSet>();
    //public List<HoldingClusterSet> HoldingClusterSets { get; set; } = new List<HoldingClusterSet>();

    //public ValueLibrary(Save save)
    //{
    //    //this.HoldingValueSets = this.GenerateHoldingValueSets(save.Holdings);
    //    this.GenerateHoldingClusterIds(save.Holdings, this.HoldingValueSets);
    //}

    //private List<HoldingValueSet> GenerateHoldingValueSets(List<Holding> holdings)
    //{
    //    List<HoldingValueSet> workingHoldingValueSets = new List<HoldingValueSet>();

    //    foreach (Holding h in holdings)
    //    {
    //        HoldingValueSet holdingValueSet = new HoldingValueSet();

    //        switch (h.TerrainType)
    //        {
    //            case TerrainType.Ocean:
    //                holdingValueSet.Terrain = -999;
    //                break;
    //            case TerrainType.Plains:
    //                holdingValueSet.Terrain = 1;
    //                break;
    //        }

    //        if (h.TerrainType != TerrainType.Ocean)
    //        {
    //            holdingValueSet.IsChokePoint = this.IsChokePoint(h);
    //        }
    //        else
    //        {
    //            holdingValueSet.IsChokePoint = false;
    //        }

    //        workingHoldingValueSets.Add(holdingValueSet);
    //    }

    //    return workingHoldingValueSets;
    //}

    //private void GenerateHoldingClusterIds(List<Holding> holdings, List<HoldingValueSet> holdingValueSets)
    //{
    //    //List<HoldingClusterSet> result = new List<HoldingClusterSet>();

    //    int clusterSize = 11;
    //    int cycleCount = 4;

    //    int maxX = holdings.Max(h => h.XPosition);
    //    int maxZ = holdings.Max(h => h.ZPosition);

    //    int minX = holdings.Min(h => h.XPosition);
    //    int minZ = holdings.Min(h => h.ZPosition);

    //    int workingMaxX = maxX;
    //    int workingMaxZ = maxZ;
    //    int workingX = maxX;
    //    int workingZ = maxZ;
    //    int workingCycleCount = cycleCount;

    //    bool isComplete = false;
    //    //int testStop = 9;
    //    UnityEngine.Debug.Log($"X:{maxX} Z:{maxZ}");

    //    int workingClusterId = 0;

    //    while (!isComplete)
    //    {
    //        //HoldingClusterSet workingHoldingClusterSet = new HoldingClusterSet();
    //        //workingHoldingClusterSet.DebugColor = Tools.RandomColor();
    //        //workingHoldingClusterSet.Holdings = new List<Holding>();
    //        workingClusterId++;

    //        HoldingCluster workingHoldingCluster = new HoldingCluster();
    //        workingHoldingCluster.DebugColor = Tools.RandomColor();
    //        workingHoldingCluster.Id = workingClusterId;

    //        //X ---> Z ----> X ---> ...ETC.
    //        for (int i = 0; i < clusterSize; i++)
    //        {
    //            Holding holding = holdings.Find(h => h.XPosition == workingX && h.ZPosition == workingZ);

    //            if (holding == null)
    //            {
    //                isComplete = true; break;
    //            }
    //            else
    //            {
    //                HoldingValueSet tempHoldingValueSet = holdingValueSets.Find(hvs => hvs.HoldingGUD == holding.GUID);
    //                //workingHoldingClusterSet.Holdings.Add(holding);
    //                //tempHoldingValueSet.ClusterId = workingClusterId;
    //                tempHoldingValueSet.HoldingCluster = workingHoldingCluster;

    //                if (workingCycleCount > 1)
    //                {
    //                    workingCycleCount--;
    //                    workingX--;

    //                    if (workingX < minX)
    //                    {
    //                        workingX = workingMaxX;
    //                        workingZ--;
    //                    }
    //                }
    //                else
    //                {
    //                    workingX = workingMaxX;
    //                    workingZ--;
    //                    workingCycleCount = cycleCount;
    //                    if (workingZ < minZ)
    //                    {
    //                        workingMaxX = workingMaxX - cycleCount;
    //                        workingX = workingMaxX;
    //                        workingZ = maxZ + minZ;
    //                        UnityEngine.Debug.Log($"X:{workingX} Z:{workingZ} ----- {maxX}");
    //                        i = clusterSize;
    //                    }
    //                }
    //            }
    //        }
    //        //result.Add(workingHoldingClusterSet);
    //    }

    //    //Oberkommando.DEBUG.GenerateClusterDebugColors(workingClusterId);
    //    //return result;
    //}

    //private bool IsChokePoint(Holding holding)
    //{
    //    bool result = false;

    //    Holding[] holdingRow1 = new Holding[] {
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition-1,holding.ZPosition+1),
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition,holding.ZPosition+1),
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition+1,holding.ZPosition+1)
    //    };

    //    Holding[] holdingRow2 = new Holding[] {
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition-1,holding.ZPosition),
    //        holding,
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition+1,holding.ZPosition)
    //    };

    //    Holding[] holdingRow3 = new Holding[] {
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition-1,holding.ZPosition-1),
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition,holding.ZPosition-1),
    //        Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition+1,holding.ZPosition-1)
    //    };

    //    bool isChokePoint = false;
    //    if ((
    //        this.IsTerrainType(holdingRow1[0], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow1[1], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow1[2], TerrainType.Ocean)
    //        ) && (
    //        this.IsTerrainType(holdingRow3[0], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow3[1], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow3[2], TerrainType.Ocean)
    //        ) && (
    //        this.NotTerrainType(holdingRow2[0], TerrainType.Ocean) &&
    //        this.NotTerrainType(holdingRow2[2], TerrainType.Ocean)
    //        )
    //    ) { isChokePoint = true; }
    //    else if ((
    //        this.IsTerrainType(holdingRow1[0], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow2[0], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow3[0], TerrainType.Ocean)
    //        ) && (
    //        this.IsTerrainType(holdingRow1[2], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow2[2], TerrainType.Ocean) ||
    //        this.IsTerrainType(holdingRow3[2], TerrainType.Ocean)
    //        ) && (
    //        this.NotTerrainType(holdingRow1[1], TerrainType.Ocean) &&
    //        this.NotTerrainType(holdingRow3[1], TerrainType.Ocean)
    //        )
    //    ) { isChokePoint = true; }

    //    result = isChokePoint;

    //    return result;
    //}

    //private bool IsTerrainType(Holding holding, TerrainType terrainType)
    //{
    //    bool result = false;

    //    if (holding != null)
    //    {
    //        result = holding.TerrainType == terrainType;
    //    }

    //    return result;
    //}

    //private bool NotTerrainType(Holding holding, TerrainType terrainType)
    //{
    //    bool result = false;

    //    if (holding != null)
    //    {
    //        result = holding.TerrainType != terrainType;
    //    }

    //    return result;
    //}
}
