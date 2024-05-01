using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] public bool IsDebugMode = false;

    private void Start()
    {
        Oberkommando.DEBUG_CONTROLLER = this;
    }
}
