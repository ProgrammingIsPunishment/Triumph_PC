using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementsView : MonoBehaviour
{
    private List<BuildingSlotView> BuildingSlotViews = new List<BuildingSlotView>();

    public void Initialize()
    {
        this.BuildingSlotViews.AddRange(this.GetComponentsInChildren<BuildingSlotView>());
        this.MarkAllSlotsEmpty();
    }

    public void UpdateView(Holding holding)
    {
        List<int> lots = new List<int>() { 1, 2, 3, 4 };

        for (int i = 0; i < holding.Buildings.Count; i++)
        {
            BuildingSlotViews[i].Refresh(holding.Buildings[i]);
            BuildingSlotViews[i].ShowDeveloped();
            lots.Remove(holding.Buildings[i].Lot);
        }

        foreach (Building b in holding.Buildings)
        {
            BuildingSlotView temp = BuildingSlotViews.First(bsv=>bsv.lot==b.Lot);
            temp.Refresh(b);
            temp.ShowDeveloped();
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
