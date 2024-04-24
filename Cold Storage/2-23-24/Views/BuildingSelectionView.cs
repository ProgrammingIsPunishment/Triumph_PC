using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingSelectionView : MonoBehaviour
{
    private List<BuildingScrollOptionButton> buildingScrollOptionButtons = new List<BuildingScrollOptionButton>();

    public void Refresh(List<Building> buildings)
    {
        for (int i = 0; i < this.buildingScrollOptionButtons.Count; i++)
        {
            if (i < buildings.Count)
            {
                this.buildingScrollOptionButtons[i].Couple(buildings[i]);
                if (Oberkommando.SELECTED_UNIT.HasRequiredResources(buildings[i].Construction.RequiredResources))
                {
                    this.buildingScrollOptionButtons[i].Display(true);
                }
                else
                {
                    this.buildingScrollOptionButtons[i].Display(false);
                }
            }
            else
            {
                this.buildingScrollOptionButtons[i].Display(false);
            }
        }
    }

    public void Display(bool isBeingShown)
    {
        this.gameObject.SetActive(isBeingShown);
    }

    public void Initialize()
    {
        this.buildingScrollOptionButtons = this.GetComponentsInChildren<BuildingScrollOptionButton>().ToList();
        foreach (BuildingScrollOptionButton bsob in this.buildingScrollOptionButtons)
        {
            bsob.Initialize();
        }
    }


}
