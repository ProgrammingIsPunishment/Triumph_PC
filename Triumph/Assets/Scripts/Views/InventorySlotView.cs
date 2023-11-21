using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image emptyImage;
    [SerializeField] private TextMeshProUGUI amountText;

    [NonSerialized] private ResourceItem resourceItem = null;

    public void Couple(ResourceItem resourceItem)
    {
        this.resourceItem = resourceItem;
        this.resourceItem.InventorySlotView = this;
    }

    public void Uncouple()
    {
        this.resourceItem.InventorySlotView = null;
        this.resourceItem = null;
    }

    public void Refresh(ResourceItem resourceItem)
    {
        this.resourceItem = resourceItem;
        this.amountText.text = resourceItem.Amount.ToString();
        this.iconImage.sprite = Resources.Load<Sprite>($"Sprites/Icons/{resourceItem.IconFileName}");
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void ClickEvent()
    {
        //switch on UIState.Gather
    }
}
