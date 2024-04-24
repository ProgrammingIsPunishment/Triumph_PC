using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementsView : MonoBehaviour
{
    [NonSerialized] public List<BuildingSlotView> BuildingSlotViews = new List<BuildingSlotView>();

    public void Initialize()
    {
        this.BuildingSlotViews.AddRange(this.GetComponentsInChildren<BuildingSlotView>());
        this.MarkAllSlotsUndeveloped();
    }

    public void Refresh(Holding holding)
    {
        List<int> lots = new List<int>() { 1, 2, 3, 4 };

        foreach (Building b in holding.Buildings)
        {
            BuildingSlotView tempBuildingSlotView = BuildingSlotViews.First(bsv=>bsv.lot==b.Lot);
            tempBuildingSlotView.Refresh(b);
            tempBuildingSlotView.Couple(b);

            if (b.Construction.IsCompleted)
            {
                tempBuildingSlotView.ShowDeveloped();
            }
            else
            {
                tempBuildingSlotView.ShowUnderConstruction();
            }

            lots.Remove(b.Lot);
        }

        foreach (int l in lots)
        {
            BuildingSlotViews.First(bsv => bsv.lot == l).ShowUndeveloped();
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

    public void MarkAllSlotsUndeveloped()
    {
        foreach (BuildingSlotView bsv in this.BuildingSlotViews)
        {
            bsv.ShowUndeveloped();
        }
    }

    public void UncoupleView()
    {
        foreach (BuildingSlotView bsv in this.BuildingSlotViews)
        {
            bsv.Uncouple();
        }
    }

    public void ShowImprovableLots(bool isBeingShown)
    {
        if (isBeingShown)
        {
            foreach (BuildingSlotView bsv in this.BuildingSlotViews)
            {
                if (!bsv.HasBuilding())
                {
                    bsv.Enable();
                    bsv.ShowSelectableForImprovement(true);
                }
            }
        }
        else
        {
            foreach (BuildingSlotView bsv in this.BuildingSlotViews)
            {
                bsv.Disable();
                bsv.ShowSelectableForImprovement(false);
            }
        }
    }

    public void ShowNeededLabor(bool isBeingShown)
    {
        if (isBeingShown)
        {
            foreach (BuildingSlotView bsv in this.BuildingSlotViews)
            {
                if (bsv.HasBuilding() && bsv.NeedsLabor())
                {
                    bsv.Enable();
                    bsv.ShowSelectableForLabor(true);
                }
            }
        }
        else
        {
            foreach (BuildingSlotView bsv in this.BuildingSlotViews)
            {
                bsv.Disable();
                bsv.ShowSelectableForLabor(false);
            }
        }
    }
}
