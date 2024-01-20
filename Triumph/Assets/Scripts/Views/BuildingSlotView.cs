using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [NonSerialized] private bool IsSelectableForImprovement = false;
    [NonSerialized] private bool IsSelectableForLabor = false;

    public void Couple(Building building)
    {
        this.building = building;
        this.building.BuildingSlotView = this;
    }

    public void Uncouple()
    {
        if (this.building != null)
        {
            this.building.BuildingSlotView = null;
            this.building = null;
        }
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

    public bool HasBuilding()
    {
        if (this.building == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool NeedsLabor()
    {
        bool result = false;

        if (this.building != null)
        {
            if (!this.building.Construction.IsCompleted)
            {
                return true;
            }
        }

        return result;
    }

    public void ShowSelectableForImprovement(bool isSelectable)
    {
        if (isSelectable)
        {
            this.IsSelectableForImprovement = true;
        }
        else
        {
            this.IsSelectableForImprovement = false;
        }
    }

    public void ShowSelectableForLabor(bool isSelectable)
    {
        if (isSelectable)
        {
            this.IsSelectableForLabor = true;
        }
        else
        {
            this.IsSelectableForLabor = false;
        }
    }

    public void ClickEvent()
    {
        if (this.IsSelectableForImprovement)
        {
            //NEED TO DO...allow selection of building
            Building tempBuilding = Oberkommando.SAVE.AllBuildings.First(b => b.GUID == "hut").CreateInstance(this.lot);
            Oberkommando.UI_CONTROLLER.LeaderBuildData(this.lot, tempBuilding);
            Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderBuild_End);
        }
        else if (this.IsSelectableForLabor)
        {
            Oberkommando.UI_CONTROLLER.LeaderLaborData(this.building);
            Oberkommando.UI_CONTROLLER.UpdateUIState(UIState.LeaderLabor_End);
        }
    }
}
