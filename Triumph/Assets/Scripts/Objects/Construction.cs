using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction
{
    public bool IsCompleted { get; set; }
    public int TurnsLeft { get; set; }
    public List<Tuple<string, int>> RequiredComponents { get; set; }
    public List<Tuple<string, int>> CurrentComponents { get; set; }

    public Construction(List<Tuple<string, int>> requiredComponents)
    {
        this.RequiredComponents = requiredComponents;
        this.CurrentComponents = new List<Tuple<string, int>>();
        this.IsCompleted = false;
        this.TurnsLeft = 0;

        this.UpdateProgress();
    }
    
    public void Labor()
    {
        this.TurnsLeft--;

        if (this.TurnsLeft <= 0)
        {
            this.Complete();
        }
    }

    private void UpdateProgress()
    {
        int tempTurnsLeftTotal = 0;

        foreach (Tuple<string, int> rc in this.RequiredComponents)
        {
            tempTurnsLeftTotal += rc.Item2;
        }

        foreach (Tuple<string, int> cc in this.CurrentComponents)
        {
            tempTurnsLeftTotal -= cc.Item2;
        }

        this.TurnsLeft = tempTurnsLeftTotal;

        if (tempTurnsLeftTotal <= 0)
        {
            this.Complete();
        }
    }

    private void Complete()
    {
        this.TurnsLeft = 0;
        this.CurrentComponents = this.RequiredComponents;
        this.IsCompleted = true;
    }
}
