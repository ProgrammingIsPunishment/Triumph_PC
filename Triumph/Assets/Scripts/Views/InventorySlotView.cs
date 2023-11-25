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
    [SerializeField] private TextMeshProUGUI usageText;

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

    public void Refresh(ResourceItem resourceItem, Attrition attrition)
    {
        this.resourceItem = resourceItem;
        this.amountText.text = resourceItem.Amount.ToString();
        this.iconImage.sprite = Resources.Load<Sprite>($"Sprites/Icons/{resourceItem.IconFileName}");
        this.Enable();

        if (attrition != null)
        {
            this.usageText.text = attrition.PerTurnConsumption.ToString();
            this.usageText.gameObject.SetActive(true);
        }
        else
        {
            this.usageText.gameObject.SetActive(false);
        }
    }

    public void Enable()
    {
        this.GetComponent<Button>().interactable = true;
        this.iconImage.color = new Color32(255, 255, 255, 255);
        this.amountText.color = new Color32(255, 255, 255, 255);
    }

    public void Disable()
    {
        this.GetComponent<Button>().interactable = false;
        this.iconImage.color = new Color32(255,255,255,50);
        this.amountText.color = new Color32(255, 255, 255, 50);
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
        if (this.GetComponent<Button>().interactable && Oberkommando.UI_CONTROLLER.CurrentUIState() == UIState.GatherLeader)
        {
            //Gather the resource
            Oberkommando.UI_CONTROLLER.GatherLeaderProcedure.SelectedResourceItem = this.resourceItem;
            Oberkommando.UI_CONTROLLER.GatherLeaderProcedure.Handle(GatherLeaderProcedureStep.Gather);
        }
    }
}
