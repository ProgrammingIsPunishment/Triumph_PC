using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColdStorageController : MonoBehaviour
{
    public GameObject ColdStorageContainer;

    [NonSerialized] private List<InventorySlotItemView> AllInventorySlotItemViews;

    public void Initialize()
    {
        this.AllInventorySlotItemViews = this.ColdStorageContainer.GetComponentsInChildren<InventorySlotItemView>().ToList();
    }

    public InventorySlotItemView GetInventorySlotItemView()
    {
        return this.AllInventorySlotItemViews.Find(isiv=>!isiv.IsUsed);
    }

    public void ReturnAllInventoryItemViews()
    {
        foreach (InventorySlotItemView isiv in this.AllInventorySlotItemViews)
        {
            isiv.Uncouple();
            isiv.transform.SetParent(this.transform);
        }
    }
}
