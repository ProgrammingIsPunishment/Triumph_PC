using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class MapController
{
    public Tuple<List<ResourceItem>, List<Holding>, List<Civilization>> LoadMapFile(string mapName)
    {
        Tuple<List<ResourceItem>, List<Holding>, List<Civilization>> result = null;
        XDocument doc = this.GetXMLFile("Maps/" + mapName);

        var allResourceItemElements = doc.Element("map").Elements("resourceitems").Elements("resourceitem");
        var allInfluentialPeopleElements = doc.Element("map").Elements("influentialpeople").Elements("influentialperson");
        var allCivilizationsElements = doc.Element("map").Elements("civilizations").Elements("civilization");
        var allHoldingsElements = doc.Element("map").Elements("holdings").Elements("holding");

        List<ResourceItem> workingResourceItems = new List<ResourceItem>();
        List<Holding> workingHoldings = new List<Holding>();
        List<Civilization> workingCivilizations = new List<Civilization>();

        List<InfluentialPerson> tempInfluentialPeople = new List<InfluentialPerson>();

        //Loop through all the resource items
        workingResourceItems.AddRange(this.ConvertResourceItems(allResourceItemElements));

        //Loop through all influential people
        tempInfluentialPeople.AddRange(this.ConvertInfluentialPeople(allInfluentialPeopleElements));

        //Loop through all the holdings
        workingHoldings.AddRange(this.ConvertHoldings(allHoldingsElements,workingResourceItems));

        //Loop through all the civilizations
        workingCivilizations.AddRange(this.ConvertCivilizations(allCivilizationsElements));

        this.AssignLeaders(ref workingCivilizations, ref tempInfluentialPeople);

        result = new Tuple<List<ResourceItem>, List<Holding>, List<Civilization>>(workingResourceItems, workingHoldings, workingCivilizations);

        return result;
    }

    private List<ResourceItem> ConvertResourceItems(IEnumerable<XElement> resourceItemElements)
    {
        List<ResourceItem> result = new List<ResourceItem>();

        //Loop through all the resource items
        foreach (var ri in resourceItemElements)
        {
            string guid = (string)ri.Attribute("guid").Value.ToLower();
            string displayName = (string)ri.Attribute("displayname").Value;
            ResourceItemType resourceItemType = Enum.Parse<ResourceItemType>(ri.Attribute("resourceitemtype").Value);
            int stackLimit = int.Parse(ri.Attribute("stacklimit").Value);
            List<Tuple<string, int>> resourceItemComponents = new List<Tuple<string, int>>();

            //loop through resource item components
            var allComponents = ri.Elements("resourceitemcomponents").Elements("resourceitemcomponent");
            foreach (var ric in allComponents)
            {
                string ricGuid = (string)ric.Attribute("guid").Value;
                int amount = int.Parse(ric.Attribute("amount").Value);

                resourceItemComponents.Add(new Tuple<string, int>(ricGuid, amount));
            }
            result.Add(new ResourceItem(guid, displayName, resourceItemType, stackLimit, resourceItemComponents));
        }

        return result;
    }

    private List<InfluentialPerson> ConvertInfluentialPeople(IEnumerable<XElement> influentialPeopleElements)
    {
        List<InfluentialPerson> result = new List<InfluentialPerson>();

        //Loop through all influential people
        foreach (var ip in influentialPeopleElements)
        {
            string guid = (string)ip.Attribute("guid").Value.ToLower();
            string displayName = (string)ip.Attribute("displayname").Value;

            result.Add(new InfluentialPerson(guid, displayName));
        }

        return result;
    }

    private List<Holding> ConvertHoldings(IEnumerable<XElement> holdingElements, List<ResourceItem> allResourceItems)
    {
        List<Holding> result = new List<Holding>();

        //Loop through all the holdings
        foreach (var h in holdingElements)
        {
            string guid = (string)h.Attribute("guid").Value.ToLower();
            string name = (string)h.Attribute("name").Value;
            int xPosition = int.Parse(h.Attribute("xposition").Value);
            int zPosition = int.Parse(h.Attribute("zposition").Value);
            TerrainType terrainType = Enum.Parse<TerrainType>(h.Attribute("terrain").Value);
            List<ResourceItem> resourceItems = new List<ResourceItem>();

            var tempResourceItems = h.Elements("resourceitems").Elements("resourceitem");

            //Load Components
            foreach (var ri in tempResourceItems)
            {
                string riGuid = (string)ri.Attribute("guid").Value;
                int amount = int.Parse(ri.Attribute("amount").Value);

                ResourceItem tempResourceItem = allResourceItems.Find(wri => wri.GUID.ToLower() == riGuid.ToLower()).CreateInstance();
                tempResourceItem.AddToStack(amount);

                resourceItems.Add(tempResourceItem);
            }

            Holding tempHolding = new Holding(name, xPosition, zPosition, terrainType, resourceItems);
            tempHolding.GUID = guid;
            result.Add(tempHolding);
        }

        return result;
    }

    private List<Civilization> ConvertCivilizations(IEnumerable<XElement> civilizationElements)
    {
        List<Civilization> result = new List<Civilization>();

        //Loop through all influential people
        foreach (var c in civilizationElements)
        {
            string guid = (string)c.Attribute("guid").Value.ToLower();
            string displayName = (string)c.Attribute("displayname").Value;

            result.Add(new Civilization(guid, displayName));
        }

        return result;
    }

    private void AssignLeaders(ref List<Civilization> civilizations, ref List<InfluentialPerson> influentialPeople)
    {
        foreach (Civilization c in civilizations)
        {
            string tempGUID = influentialPeople[0].GUID;
            InfluentialPerson influentialPerson = influentialPeople.Find(ip=>ip.GUID == tempGUID);
            influentialPerson.IsLeader = true;
            influentialPeople.RemoveAt(0);
            c.InfluentialPeople.Add(influentialPerson);
        }
    }

    private XDocument GetXMLFile(string filePath)
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(filePath);
        var doc = XDocument.Parse(txtAsset.text);
        return doc;
    }
}
