using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopSlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Oberkommando.UI_CONTROLLER.tooltipView.Refresh("Pop",$"Happiness: {this.Pop.Happiness} Necessities: {this.Pop.Necessities}");
        Oberkommando.UI_CONTROLLER.tooltipView.Display(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Oberkommando.UI_CONTROLLER.tooltipView.Display(false);
    }
}
