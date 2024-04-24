using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDisplayManager : MonoBehaviour
{
    public void Show(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }
}
