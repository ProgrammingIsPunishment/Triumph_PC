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

            switch (h.TerrainType)
            {
                case TerrainType.Ocean:
                    holdingValueSet.Terrain = 0;
                    break;
                case TerrainType.Plains:
                    holdingValueSet.Terrain = 1;
                    break;
            }

            //Determine chokepoints
            Holding[,] holdingMatrix = new Holding[,] {
                { 
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition-1,h.ZPosition+1),
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition,h.ZPosition+1),
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition+1,h.ZPosition+1) 
                },
                {
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition-1,h.ZPosition), 
                    h,
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition+1,h.ZPosition)
                },
                {
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition-1,h.ZPosition-1),
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition,h.ZPosition-1),
                    Oberkommando.UTILITIES_SERVICE.GetHoldingAtPosition(h.XPosition+1,h.ZPosition-1)
                }
            };

            //if (holdingMatrix[0,0].TerrainType == TerrainType.Ocean ||)
            //{

            //}


            workingHoldingValueSets.Add(holdingValueSet);
        }

        return workingHoldingValueSets;
    }
}
