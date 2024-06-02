using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ValueLibrary
{
    public List<HoldingValueSet> HoldingValueSets { get; set; } = new List<HoldingValueSet>();
    public ValueLibrary(Save save)
    {
        this.HoldingValueSets = this.GenerateHoldingValueSets(save.Holdings);
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

        //bool?[] holdingValue = new bool?[]
        //{
        //    holding.TerrainType == TerrainType.Ocean, holding.TerrainType == TerrainType.Ocean, holding.TerrainType == TerrainType.Ocean,
        //    holding.TerrainType == TerrainType.Ocean, null, holding.TerrainType == TerrainType.Ocean,
        //    holding.TerrainType == TerrainType.Ocean, holding.TerrainType == TerrainType.Ocean, holding.TerrainType == TerrainType.Ocean
        //};

        //List<bool?[]> patterns = new List<bool?[]>()
        //{
        //    new bool?[]
        //    {
        //        true,true,true,     //X X X
        //        false,null,false,   //O O O
        //        true,true,true      //X X X
        //    },
        //    new bool?[]
        //    {
        //        true,false,true,    //X O X
        //        true,null,true,     //X O X
        //        true,true,true      //X O X
        //    },
        //    new bool?[]
        //    {
        //        true,false,true,    //X X X
        //        true,null,true,     //X O X
        //        true,true,true      //X X X
        //    },
        //};

        //Determine chokepoints

        if (holding.Name == "Madrid")
        {
            UnityEngine.Debug.Log("Hit");
        }

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
