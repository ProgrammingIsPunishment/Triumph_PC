using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIProcess
{
    public abstract UIState UIState { get; }

    public abstract void ProcessEnd();
}
