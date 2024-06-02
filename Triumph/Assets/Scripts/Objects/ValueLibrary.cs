using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ValueLibrary
{
    public List<HoldingValueSet> HoldingValueSets { get; set; } = new List<HoldingValueSet>();
    public List<HoldingClusterSet> HoldingClusterSets { get; set; } = new List<HoldingClusterSet>();

    public ValueLibrary(Save save)
    {
        this.HoldingValueSets = this.GenerateHoldingValueSets(save.Holdings);
        this.HoldingClusterSets = this.GenerateHoldingClusterSets(save.Holdings);
    }

    private List<HoldingClusterSet> GenerateHoldingClusterSets(List<Holding> holdings)
    {
        List<HoldingClusterSet> result = new List<HoldingClusterSet>();

        int clusterSize = 11;
        int cycleCount = 4;

        int maxX = holdings.Max(h=>h.XPosition);
        int maxZ = holdings.Max(h=>h.ZPosition);

        int minX = holdings.Min(h => h.XPosition);
        int minZ = holdings.Min(h => h.ZPosition);

        int workingMaxX = maxX;
        int workingMaxZ = maxZ;
        int workingX = maxX;
        int workingZ = maxZ;
        int workingCycleCount = cycleCount;

        bool isComplete = false;
        //int testStop = 9;
        UnityEngine.Debug.Log($"X:{maxX} Z:{maxZ}");

        while (!isComplete)
        {
            HoldingClusterSet workingHoldingClusterSet = new HoldingClusterSet();
            workingHoldingClusterSet.DebugColor = Tools.RandomColor();
            workingHoldingClusterSet.Holdings = new List<Holding>();

            //X ---> Z ----> X ---> ...ETC.
            for (int i = 0; i < clusterSize; i++)
            {
                Holding holding = holdings.Find(h=>h.XPosition == workingX && h.ZPosition == workingZ);

                if (holding == null) 
                {
                    isComplete = true; break;
                }
                else
                {
                    workingHoldingClusterSet.Holdings.Add(holding);

                    if (workingCycleCount > 1)
                    {
                        workingCycleCount--;
                        workingX--;

                        if (workingX < minX)
                        {
                            workingX = workingMaxX;
                            workingZ--;
                        }
                    }
                    else
                    {
                        workingX = workingMaxX;
                        workingZ--;
                        workingCycleCount = cycleCount;
                        if (workingZ < minZ)
                        {
                            workingMaxX = workingMaxX - cycleCount;
                            workingX = workingMaxX;
                            workingZ = maxZ + minZ;
                            UnityEngine.Debug.Log($"X:{workingX} Z:{workingZ} ----- {maxX}");
                            i = clusterSize;
                        }
                    }
                }
            }

            result.Add(workingHoldingClusterSet);
        }

        return result;
    }

    private List<HoldingValueSet> GenerateHoldingValueSets(List<Holding> holdings)
    {
        List<HoldingValueSet> workingHoldingValueSets = new List<HoldingValueSet>();

        foreach (Holding h in holdings)
        {
            HoldingValueSet holdingValueSet = new HoldingValueSet();

            holdingValueSet.HoldingGUD = h.GUID;

            switch (h.TerrainType)
            {
                case TerrainType.Ocean:
                    holdingValueSet.Terrain = 999;
                    break;
                case TerrainType.Plains:
                    holdingValueSet.Terrain = 1;
                    break;
            }

            if (h.TerrainType != TerrainType.Ocean)
            {
                holdingValueSet.IsChokePoint = this.IsChokePoint(h);
            }
            else
            {
                holdingValueSet.IsChokePoint = false;
            }

            workingHoldingValueSets.Add(holdingValueSet);
        }

        return workingHoldingValueSets;
    }

    private bool IsChokePoint(Holding holding)
    {
        bool result = false;

        Holding[] holdingRow1 = new Holding[] {
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition-1,holding.ZPosition+1),
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition,holding.ZPosition+1),
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition+1,holding.ZPosition+1)
        };

        Holding[] holdingRow2 = new Holding[] {
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition-1,holding.ZPosition),
            holding,
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition+1,holding.ZPosition)
        };

        Holding[] holdingRow3 = new Holding[] {
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition-1,holding.ZPosition-1),
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition,holding.ZPosition-1),
            Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(holding.XPosition+1,holding.ZPosition-1)
        };

        bool isChokePoint = false;
        if ((
            this.IsTerrainType(holdingRow1[0], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow1[1], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow1[2], TerrainType.Ocean)
            ) && (
            this.IsTerrainType(holdingRow3[0], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow3[1], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow3[2], TerrainType.Ocean)
            ) && (
            this.NotTerrainType(holdingRow2[0], TerrainType.Ocean) &&
            this.NotTerrainType(holdingRow2[2], TerrainType.Ocean)
            )
        ) { isChokePoint = true; }
        else if ((
            this.IsTerrainType(holdingRow1[0], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow2[0], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow3[0], TerrainType.Ocean)
            ) && (
            this.IsTerrainType(holdingRow1[2], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow2[2], TerrainType.Ocean) ||
            this.IsTerrainType(holdingRow3[2], TerrainType.Ocean)
            ) && (
            this.NotTerrainType(holdingRow1[1], TerrainType.Ocean) &&
            this.NotTerrainType(holdingRow3[1], TerrainType.Ocean)
            )
        ) { isChokePoint = true; }

        result = isChokePoint;

        return result;
    }

    private bool IsTerrainType(Holding holding, TerrainType terrainType)
    {
        bool result = false;

        if (holding != null)
        {
            result = holding.TerrainType == terrainType;
        }

        return result;
    }

    private bool NotTerrainType(Holding holding, TerrainType terrainType)
    {
        bool result = false;

        if (holding != null)
        {
            result = holding.TerrainType != terrainType;
        }

        return result;
    }
}
