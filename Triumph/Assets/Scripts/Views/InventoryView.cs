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
    public Inventory Inventory { get; set; }

    public void Initialize()
    {
        this.InventorySlotViews.AddRange(this.GetComponentsInChildren<InventorySlotView>());
        this.ShowAllSlotsEmpty();
    }

    public void Couple(Inventory inventory)
    {
        this.Inventory = inventory;
    }

    public void Uncouple()
    {
        this.Inventory = null;
    }

    /// <summary>
    /// Natural Resources
    /// </summary>
    /// <param name="inventory"></param>
    public void Refresh(Inventory inventory)
    {
        this.HideAllSlots();
        for (int i = 0; i < inventory.ResourceItems.Count; i++)
        {
            //this.InventorySlotViews[i].Refresh(inventory.ResourceItems[i]);
            //this.InventorySlotViews[i].Couple(inventory.ResourceItems[i]);
            //this.InventorySlotViews[i].ShowFull();

            InventorySlotItemView workingInventorySlotItemView = Oberkommando.COLDSTORAGE_CONTROLLER.GetInventorySlotItemView();
            workingInventorySlotItemView.Refresh(inventory.ResourceItems[i]);
            workingInventorySlotItemView.Couple(inventory.ResourceItems[i]);
            this.InventorySlotViews[i].Couple(workingInventorySlotItemView);
            this.InventorySlotViews[i].AddInventorySlotItemView(workingInventorySlotItemView);
        }
    }

    /// <summary>
    /// Unit Supply
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="supplies"></param>
    public void Refresh(Inventory inventory, Supply supplies)
    {
        this.ShowAllSlotsEmpty();
        for (int i = 0; i < inventory.ResourceItems.Count; i++)
        {
            InventorySlotItemView workingInventorySlotItemView = Oberkommando.COLDSTORAGE_CONTROLLER.GetInventorySlotItemView();
            Attrition tempAttrition = supplies.Attritions.FirstOrDefault(a => a.ResourceItemGUID == inventory.ResourceItems[i].GUID);
            workingInventorySlotItemView.Refresh(inventory.ResourceItems[i], tempAttrition);
            workingInventorySlotItemView.Couple(inventory.ResourceItems[i]);
            this.InventorySlotViews[i].Couple(workingInventorySlotItemView);
            this.InventorySlotViews[i].AddInventorySlotItemView(workingInventorySlotItemView);
            //Attrition tempAttrition = supplies.Attritions.FirstOrDefault(a => a.ResourceItemGUID == inventory.ResourceItems[i].GUID);
            //this.InventorySlotViews[i].Refresh(inventory.ResourceItems[i], tempAttrition);
            //this.InventorySlotViews[i].Couple(inventory.ResourceItems[i]);
            //this.InventorySlotViews[i].ShowFull();
        }
    }

    /// <summary>
    /// Population goods
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="supplies"></param>
    public void Refresh(Inventory inventory, GoodsTemplate goodsTemplate)
    {
        this.ShowAllSlotsEmpty();
        for (int i = 0; i < inventory.ResourceItems.Count; i++)
        {
            //Good tempGood = goodsTemplate.Goods.FirstOrDefault(g => g.ResourceItemGUID == inventory.ResourceItems[i].GUID);
            //this.InventorySlotViews[i].Refresh(inventory.ResourceItems[i], tempGood);
            //this.InventorySlotViews[i].Couple(inventory.ResourceItems[i]);
            //this.InventorySlotViews[i].ShowFull();

            InventorySlotItemView workingInventorySlotItemView = Oberkommando.COLDSTORAGE_CONTROLLER.GetInventorySlotItemView();
            Good tempGood = goodsTemplate.Goods.FirstOrDefault(g => g.ResourceItemGUID == inventory.ResourceItems[i].GUID);
            workingInventorySlotItemView.Refresh(inventory.ResourceItems[i], tempGood);
            workingInventorySlotItemView.Couple(inventory.ResourceItems[i]);
            this.InventorySlotViews[i].Couple(workingInventorySlotItemView);
            this.InventorySlotViews[i].AddInventorySlotItemView(workingInventorySlotItemView);
        }
    }

    public void HideAllSlots()
    {
        //foreach (InventorySlotView isv in this.InventorySlotViews)
        //{
        //    isv.Hide();
        //}
    }

    public void ShowAllSlotsEmpty()
    {
        //foreach (InventorySlotView isv in this.InventorySlotViews)
        //{
        //    isv.ShowEmpty();
        //}
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
        //foreach (InventorySlotView isv in this.InventorySlotViews)
        //{
        //    isv.Uncouple();
        //}
    }

    public void ShowGatherableResources(bool isBeingShown)
    {
        if (isBeingShown)
        {
            ////Eventually will need to disable and enable only items that the unit can gather
            ////For right now, everything is enabled for the sake of testing
            foreach (InventorySlotView isv in this.InventorySlotViews)
            {
                //isv.Enable();
                if (isv.inventorySlotItemView != null)
                {
                    isv.inventorySlotItemView.SetSelectable(true);
                }
            }
        }
        else
        {
            foreach (InventorySlotView isv in this.InventorySlotViews)
            {
                //isv.Disable();
                if (isv.inventorySlotItemView != null)
                {
                    isv.inventorySlotItemView.SetSelectable(false);
                }
            }
        }
    }
}
