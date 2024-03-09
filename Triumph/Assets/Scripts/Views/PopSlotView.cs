using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopSlotView : MonoBehaviour
{
    [NonSerialized] public Pop Pop;

    public void Couple(Pop pop)
    {
        this.Pop = pop;
    }

    public void Display(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }
}
