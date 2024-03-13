using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class Tools
{
    public static bool IsFirstNumberGreater(int first, int second)
    {
        return first > second ? true : false;
    }

    public static bool GetRandomBoolean()
    {
        return new Random().Next(100) <= 50 ? true : false;
    }
}
