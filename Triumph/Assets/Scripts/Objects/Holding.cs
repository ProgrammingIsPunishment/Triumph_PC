using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Holding
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public int XPosition { get; set; }
    public int ZPosition { get; set; }
    public TerrainType TerrainType { get; set; }
    public Inventory NaturalResourcesInventory { get; set; }
    public Inventory StorageInventory { get; set; }
    public List<Holding> AdjacentHoldings { get; set; }
    public VisibilityLevel VisibilityLevel { get; set; }
    public List<Building> Buildings { get; set; }
    public Population Population { get; set; }

    [NonSerialized] public HoldingDisplayManager HoldingDisplayManager;

    public Holding(string displayName, int xPosition, int ZPosition, TerrainType terrainType, Inventory naturalResourcesInventory, Inventory storageInventory, Population population)
    {
        this.DisplayName = displayName;
        this.XPosition = xPosition;
        this.ZPosition = ZPosition;
        this.TerrainType = terrainType;
        this.NaturalResourcesInventory = naturalResourcesInventory;
        this.StorageInventory = storageInventory;
        this.AdjacentHoldings = new List<Holding>();
        this.Buildings = new List<Building>();
        this.Population = population;
    }

    public void PassEffectFromHolding()
    {
        if (this.Population.Amount > 0)
        {
            //Determine values from buildings
            float housingCount = 0;
            foreach (Building b in this.Buildings)
            {
                if (b.Construction.IsCompleted)
                {
                    foreach (Attribute a in b.Attributes)
                    {
                        switch (a.AttributeType)
                        {
                            case AttributeType.Housing:
                                housingCount += a.Value;
                                break;
                            default:
                                //Do nothing...
                                break;
                        }
                    }
                }
            }


            if (housingCount >= this.Population.Amount)
            {
                this.Population.AddEffect("housed");
                this.Population.RemoveEffect("homeless");
            }
            else
            {
                this.Population.RemoveEffect("housed");
                this.Population.AddEffect("homeless");
            }
        }
    }

    public void BuildBuilding(Building building)
    {
        this.Buildings.Add(building);
    }

    public void UpdateVisibility(Civilization civilization)
    {
        if (!civilization.ExploredHoldings.Contains(this))
        {
            civilization.ExploredHoldings.Add(this);
            this.VisibilityLevel = VisibilityLevel.Explored;
            this.HoldingDisplayManager.ShowExplored(true);
        }

        foreach (Holding h in this.AdjacentHoldings)
        {
            if (!civilization.ExploredHoldings.Contains(h))
            {
                h.HoldingDisplayManager.ShowExplored(false);
                h.HoldingDisplayManager.Show(true);
            }
        }
    }

    public bool HasUnconstructedBuildings()
    {
        bool result = false;

        foreach (Building b in this.Buildings)
        {
            if (!b.Construction.IsCompleted)
            {
                result = true;
            }
        }

        return result;
    }

    public bool HasUndevelopedLots()
    {
        bool result = false;

        //NEED TO DO...needs to be more robust as some building take up multiple slots
        if (this.Buildings.Count < 4) { result = true; }

        return result;
    }
}
