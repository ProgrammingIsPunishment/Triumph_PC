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
        this.MarkAllSlotsEmpty();
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

    public void MarkAllSlotsEmpty()
    {
        foreach (BuildingSlotView bsv in this.BuildingSlotViews)
        {
            bsv.ShowUndeveloped();
        }
    }
}
