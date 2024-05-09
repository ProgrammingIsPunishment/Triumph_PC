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
}
