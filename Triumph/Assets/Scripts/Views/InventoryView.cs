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

    /// <summary>
    /// Natural Resources
    /// </summary>
    /// <param name="inventory"></param>
    public void Refresh(Inventory inventory)
    {
        for (int i = 0; i < inventory.ResourceItems.Count; i++)
        {
            this.InventorySlotViews[i].Refresh(inventory.ResourceItems[i]);
            this.InventorySlotViews[i].Couple(inventory.ResourceItems[i]);
            this.InventorySlotViews[i].Show();
        }
    }

    /// <summary>
    /// Unit Supply
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="supplies"></param>
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

    /// <summary>
    /// Population goods
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="supplies"></param>
    public void Refresh(Inventory inventory, GoodsTemplate goodsTemplate)
    {
        for (int i = 0; i < inventory.ResourceItems.Count; i++)
        {
            Good tempGood = goodsTemplate.Goods.FirstOrDefault(g => g.ResourceItemGUID == inventory.ResourceItems[i].GUID);
            this.InventorySlotViews[i].Refresh(inventory.ResourceItems[i], tempGood);
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

    public void UncoupleView()
    {
        foreach (InventorySlotView isv in this.InventorySlotViews)
        {
            isv.Uncouple();
        }
    }

    public void ShowGatherableResources(bool isBeingShown)
    {
        if (isBeingShown)
        {
            //Eventually will need to disable and enable only items that the unit can gather
            //For right now, everything is enabled for the sake of testing
            foreach (InventorySlotView isv in this.InventorySlotViews)
            {
                isv.Enable();
                isv.ShowSelectable(true);
            }
        }
        else
        {
            foreach (InventorySlotView isv in this.InventorySlotViews)
            {
                isv.Disable();
                isv.ShowSelectable(false);
            }
        }
    }
}
