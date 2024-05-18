using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindowManager : MonoBehaviour, IDragHandler
{
    public Canvas Canvas;

    public RectTransform RectTransform;

    void Start()
    {
        RectTransform = this.GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.RectTransform.anchoredPosition += eventData.delta / this.Canvas.scaleFactor;
    }
}
