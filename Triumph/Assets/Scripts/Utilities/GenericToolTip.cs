using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GenericToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string title;
    [SerializeField] private string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Oberkommando.UI_CONTROLLER.tooltipView.Refresh(this.title,this.message);
        Oberkommando.UI_CONTROLLER.tooltipView.Display(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Oberkommando.UI_CONTROLLER.tooltipView.Display(false);
    }
}
