using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour, IDropHandler
{
    [NonSerialized] public InventorySlotItemView inventorySlotItemView = null;

    public void Couple(InventorySlotItemView inventorySlotItemView)
    {
        this.inventorySlotItemView = inventorySlotItemView;
    }

    public void Uncouple()
    {
        this.inventorySlotItemView = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("HIT!!!!!!!!!!ASDASDASD!");
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }

    public void AddInventorySlotItemView(InventorySlotItemView inventorySlotItemView)
    {
        inventorySlotItemView.transform.SetParent(this.transform);
    }
}