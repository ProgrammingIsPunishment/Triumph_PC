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
    [SerializeField] private GameObject constructionOverlayObject;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] public int lot;

    [NonSerialized] public Building building = null;

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


        if (!building.Construction.IsCompleted)
        {
            this.turnsText.text = building.Construction.TurnsLeft.ToString();
        }
    }

    public void ShowDeveloped()
    {
        this.emptyImage.gameObject.SetActive(false);
        this.constructionOverlayObject.SetActive(false);
        this.iconImage.gameObject.SetActive(true);
        this.iconImage.color = new Color32(255, 255, 255, 255);
    }

    public void ShowUnderConstruction()
    {
        this.emptyImage.gameObject.SetActive(false);
        this.iconImage.gameObject.SetActive(true);
        this.iconImage.color = new Color32(255,255,255,100);
        this.constructionOverlayObject.SetActive(true);
    }

    public void ShowUndeveloped()
    {
        this.nameText.text = string.Empty;
        this.emptyImage.gameObject.SetActive(true);
        this.constructionOverlayObject.SetActive(false);
        this.iconImage.gameObject.SetActive(false);
    }

    public void Enable()
    {
        this.GetComponent<Button>().interactable = true;
    }

    public void Disable()
    {
        this.GetComponent<Button>().interactable = false;
    }

    public void ClickEvent()
    {
        if (this.GetComponent<Button>().interactable && Oberkommando.UI_CONTROLLER.CurrentUIState() == UIState.ConstructLeader)
        {
            Oberkommando.UI_CONTROLLER.ConstructLeaderProcedure.SelectedLot = this.lot;
            Oberkommando.UI_CONTROLLER.ConstructLeaderProcedure.Handle(ConstructLeaderProcedureStep.Construct);
        }
    }
}
