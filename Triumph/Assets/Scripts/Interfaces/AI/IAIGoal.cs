using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIGoal
{
    public bool IsCompleted { get; set; }
    public bool IsActive { get; set; }
}
