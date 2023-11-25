using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private List<InventorySlotView> InventorySlotViews = new List<InventorySlotView>();

    public void Initialize()
    {
        this.InventorySlotViews.AddRange(this.GetComponentsInChildren<InventorySlotView>());
        this.HideAllSlots();
    }

    public void Refresh(Inventory inventory, Supply supplies)
    {
        for (int i = 0; i < inventory.ResourceItems.Count; i++)
        {
            Attrition tempAttrition = supplies.Attritions.FirstOrDefault(a => a.ResourceItemGUID == inventory.ResourceItems[i].GUID);
            this.InventorySlotViews[i].Refresh(inventory.ResourceItems[i], tempAttrition);
            this.InventorySlotViews[i].Couple(inventory.ResourceItems[i]);
            this.InventorySlotViews[i].Show();
        }
    }

    public void HideAllSlots()
    {
        foreach (InventorySlotView isv in this.InventorySlotViews)
        {
            isv.Hide();
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
