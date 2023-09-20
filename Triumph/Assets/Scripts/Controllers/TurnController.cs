using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController
{
    public void EndTurn()
    {
        Oberkommando.SAVE.Turn++;
        Debug.Log(Oberkommando.SAVE.Turn.ToString());
    }
}
