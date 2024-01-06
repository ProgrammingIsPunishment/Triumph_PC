using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlotView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image emptyImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] public int lot;

    [NonSerialized] private Building building = null;

    public void Couple(Building building)
    {
        this.building = building;
        this.building.BuildingSlotView = this;
    }

    public void Uncouple()
    {
        this.building.BuildingSlotView = null;
        this.building = null;
    }

    public void Refresh(Building building)
    {
        this.building = building;
        this.nameText.text = building.DisplayName.ToString();
        this.iconImage.sprite = Resources.Load<Sprite>($"Sprites/Buildings/{building.IconFileName}");
    }

    public void ShowDeveloped()
    {
        this.emptyImage.gameObject.SetActive(false);
        this.iconImage.gameObject.SetActive(true);
    }

    public void ShowUndeveloped()
    {
        this.nameText.text = string.Empty;
        this.emptyImage.gameObject.SetActive(true);
        this.iconImage.gameObject.SetActive(false);
    }

    public void ClickEvent()
    {
        //if (this.GetComponent<Button>().interactable && Oberkommando.UI_CONTROLLER.CurrentUIState() == UIState.GatherLeader)
        //{
        //    //Gather the resource
        //    Oberkommando.UI_CONTROLLER.GatherLeaderProcedure.SelectedResourceItem = this.resourceItem;
        //    Oberkommando.UI_CONTROLLER.GatherLeaderProcedure.Handle(GatherLeaderProcedureStep.Gather);
        //}
    }
}
