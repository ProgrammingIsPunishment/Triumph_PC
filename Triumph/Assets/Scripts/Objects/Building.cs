using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class Building
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public string IconFileName { get; set; }
    public string ModelFileName { get; set; }
    public Construction Construction { get; set; }
    public LayoutSize LayoutSize { get; set; }
    public int Lot { get; set; }
    public List<Attribute> Attributes { get; set; }

    [NonSerialized] public BuildingSlotView BuildingSlotView;

    public Building(string guid, string displayName, LayoutSize layoutSize, string iconFileName, string modelFileName, Construction construction, List<Attribute> attributes)
    {
        this.GUID = guid;
        this.DisplayName = displayName;
        this.LayoutSize = layoutSize;
        this.IconFileName = iconFileName;
        this.ModelFileName = modelFileName;
        this.Construction = construction;
        this.Attributes = attributes;
    }

    public Building CreateInstance(int lot)
    {
        Construction tempConstruction = new Construction(this.Construction.RequiredComponents,this.Construction.RequiredResources);
        Building tempBuilding = new Building(this.GUID, this.DisplayName, this.LayoutSize, this.IconFileName, this.ModelFileName, tempConstruction, this.Attributes);
        tempBuilding.Lot = lot;
        return tempBuilding;
    }
}
