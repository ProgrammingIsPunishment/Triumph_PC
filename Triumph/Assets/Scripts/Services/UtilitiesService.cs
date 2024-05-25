using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UtilitiesService : MonoBehaviour
{
    public Unit GetUnitAtLocation(int x, int z)
    {
        return Oberkommando.SAVE.Units.FirstOrDefault(u=>u.XPosition == x && u.ZPosition == z);
    }

    public Holding GetHoldingAtPosition(int x, int z)
    {
        return Oberkommando.SAVE.Holdings.FirstOrDefault(h => h.XPosition == x && h.ZPosition == z);
    }

    public Holding GetHoldingByName(string name)
    {
        return Oberkommando.SAVE.Holdings.FirstOrDefault(h => h.Name.ToUpper() == name.ToUpper());
    }

    public Holding GetHoldingByGUID(string guid)
    {
        return Oberkommando.SAVE.Holdings.FirstOrDefault(h => h.GUID.ToUpper() == guid.ToUpper());
    }
}
