using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITerritorialExpansionGoal : IAIGoal
{
    public bool IsCompleted { get; set; }
    public bool IsActive { get; set; }
    public List<Holding> HoldingsToCapture { get; set; }

    public AITerritorialExpansionGoal(List<Holding> holdingsToCapture)
    {
        this.IsCompleted = false;
        this.IsActive = true;
        this.HoldingsToCapture = holdingsToCapture;
    }
}
