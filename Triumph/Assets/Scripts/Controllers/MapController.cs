using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MapController
{
    private List<Holding> spawnHoldings = new List<Holding>();
    private Dictionary<string, TerrainType> terrainDictionary = new Dictionary<string, TerrainType>();
    private Dictionary<string, int> heatDictionary = new Dictionary<string, int>();
    private Dictionary<string, string> resourceDictionary = new Dictionary<string, string>();
    private Dictionary<Tuple<int,int>, Tuple<string, string>> holdingDictionary = new Dictionary<Tuple<int, int>, Tuple<string, string>>();
    private Dictionary<string, string> spawnDictionary = new Dictionary<string, string>();

    public Tuple<List<ResourceItem>, List<Holding>, List<Civilization>> LoadMapFile(string mapName)
    {
        Tuple<List<ResourceItem>, List<Holding>, List<Civilization>> result = null;
        XDocument doc = this.GetXMLFile($"Maps/{mapName}/{mapName}_manifest");

        var allSpawnDefinitionElements = doc.Element("map").Elements("spawns").Elements("spawn");
        var allHeatDefinitionElements = doc.Element("map").Elements("heats").Elements("heat");
        var allTerrainDefinitionElements = doc.Element("map").Elements("terrains").Elements("terrain");

        var allResourceItemElements = doc.Element("map").Elements("resourceitems").Elements("resourceitem");
        var allInfluentialPeopleElements = doc.Element("map").Elements("influentialpeople").Elements("influentialperson");
        var allCivilizationsElements = doc.Element("map").Elements("civilizations").Elements("civilization");
        var allHoldingsElements = doc.Element("map").Elements("holdings").Elements("holding");

        List<ResourceItem> workingResourceItems = new List<ResourceItem>();
        List<Holding> workingHoldings = new List<Holding>();
        List<Civilization> workingCivilizations = new List<Civilization>();

        List<InfluentialPerson> tempInfluentialPeople = new List<InfluentialPerson>();

        //Generate independent definition dictionaries
        this.heatDictionary = this.ConvertToHeatDictionary(allHeatDefinitionElements);
        this.terrainDictionary = this.ConvertToTerrainDictionary(allTerrainDefinitionElements);
        this.holdingDictionary = this.ConvertToHoldingDictionary(allHoldingsElements);
        this.spawnDictionary = this.ConvertToSpawnDictionary(allSpawnDefinitionElements);

        //Loop through all the resource items
        workingResourceItems.AddRange(this.ConvertToResourceItems(allResourceItemElements));

        //Loop through all influential people
        tempInfluentialPeople.AddRange(this.ConvertToInfluentialPeople(allInfluentialPeopleElements));

        //Loop through all the holdings
        workingHoldings.AddRange(this.ReadMapTextures(mapName, workingResourceItems));

        //Loop through all the civilizations
        workingCivilizations.AddRange(this.ConvertToCivilizations(allCivilizationsElements));

        this.AssignLeadersToCivilizations(ref workingCivilizations, ref tempInfluentialPeople);

        this.GenerateLeaderUnits(ref workingCivilizations, ref workingHoldings);
        this.AssignAdjacentHoldings(workingHoldings);

        result = new Tuple<List<ResourceItem>, List<Holding>, List<Civilization>>(workingResourceItems, workingHoldings, workingCivilizations);

        return result;
    }

    private Dictionary<string, TerrainType> ConvertToTerrainDictionary(IEnumerable<XElement> terrainDefinitionElements)
    {
        Dictionary<string, TerrainType> result = new Dictionary<string, TerrainType>();

        //Loop through all the terrains
        foreach (var td in terrainDefinitionElements)
        {
            TerrainType terrainType = Enum.Parse<TerrainType>(td.Attribute("terrain").Value);
            string colorHexCode = (string)td.Attribute("hexcode").Value;

            result.Add(colorHexCode, terrainType);
        }

        return result;
    }

    private Dictionary<Tuple<int, int>, Tuple<string, string>> ConvertToHoldingDictionary(IEnumerable<XElement> holdingDefinitionElements)
    {
        Dictionary<Tuple<int, int>, Tuple<string, string>> result = new Dictionary<Tuple<int, int>, Tuple<string, string>>();

        //Loop through all the terrains
        foreach (var hd in holdingDefinitionElements)
        {
            string guid = (string)hd.Attribute("guid").Value.ToLower();
            string name = (string)hd.Attribute("displayname").Value;
            int xPosition = int.Parse(hd.Attribute("xposition").Value);
            int zPosition = int.Parse(hd.Attribute("zposition").Value);

            result.Add(new Tuple<int,int>(xPosition,zPosition), new Tuple<string, string>(guid, name));
        }

        return result;
    }

    private Dictionary<string, int> ConvertToHeatDictionary(IEnumerable<XElement> heatDefinitionElements)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        //Loop through all the terrains
        foreach (var hd in heatDefinitionElements)
        {
            int amount = int.Parse(hd.Attribute("amount").Value);
            string colorHexCode = (string)hd.Attribute("hexcode").Value;

            result.Add(colorHexCode, amount);
        }

        return result;
    }

    private Dictionary<string, string> ConvertToSpawnDictionary(IEnumerable<XElement> spawnDefinitionElements)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        //Loop through all the terrains
        foreach (var sd in spawnDefinitionElements)
        {
            string colorHexCode = (string)sd.Attribute("hexcode").Value;
            string group = (string)sd.Attribute("group").Value;

            result.Add(colorHexCode, group);
        }

        return result;
    }

    private List<Holding> ReadMapTextures(string mapName, List<ResourceItem> allResourceItems)
    {
        Texture2D terrainTexture = Resources.Load<Texture2D>($"Maps/{mapName}/{mapName}_terrain");
        Texture2D resources1Texture = Resources.Load<Texture2D>($"Maps/{mapName}/{mapName}_resources_1");
        Texture2D resources1HeatTexture = Resources.Load<Texture2D>($"Maps/{mapName}/{mapName}_resources_1_heat");
        Texture2D spawnsTexture = Resources.Load<Texture2D>($"Maps/{mapName}/{mapName}_spawns");

        List<Holding> result = new List<Holding>();
        int width = terrainTexture.width;
        int height = terrainTexture.height;;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                List<ResourceItem> resourceItems = new List<ResourceItem>();

                string terrainColorHex = ColorUtility.ToHtmlStringRGB(terrainTexture.GetPixel(x, z));
                string resources1ColorHex = ColorUtility.ToHtmlStringRGB(resources1Texture.GetPixel(x, z));
                string resources1HeatColorHex = ColorUtility.ToHtmlStringRGB(resources1HeatTexture.GetPixel(x, z));
                string spawnColorHex = ColorUtility.ToHtmlStringRGB(spawnsTexture.GetPixel(x, z));

                bool foundHolding = this.holdingDictionary.TryGetValue(new Tuple<int,int>(x,z), out Tuple<string,string> guidAndDisplayName);
                bool foundTerrain = this.terrainDictionary.TryGetValue(terrainColorHex,out TerrainType terrainType);
                bool foundResource1 = this.resourceDictionary.TryGetValue(resources1ColorHex, out string resource1GUID);
                bool foundHeat1 = this.heatDictionary.TryGetValue(resources1HeatColorHex, out int amount1);
                bool foundSpawn = this.spawnDictionary.TryGetValue(spawnColorHex, out string spawnGroup);

                if (foundResource1)
                {
                    ResourceItem tempResourceItem = allResourceItems.Find(wri => wri.GUID.ToLower() == resource1GUID.ToLower()).CreateInstance();

                    if (foundHeat1)
                    {
                        tempResourceItem.AddToStack(amount1);
                    }

                    resourceItems.Add(tempResourceItem);
                }

                Holding tempHolding = new Holding(guidAndDisplayName.Item2,x,z,terrainType, resourceItems);
                tempHolding.GUID = guidAndDisplayName.Item1;

                if (foundSpawn)
                {
                    this.spawnHoldings.Add(tempHolding);
                }

                result.Add(tempHolding);
            }
        }

        return result;
    }

    private List<ResourceItem> ConvertToResourceItems(IEnumerable<XElement> resourceItemElements)
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

            string hexcode = (string)ri.Attribute("hexcode").Value;
            if (hexcode != "null") { this.resourceDictionary.Add(hexcode, guid); }

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

    private List<InfluentialPerson> ConvertToInfluentialPeople(IEnumerable<XElement> influentialPeopleElements)
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

    private List<Civilization> ConvertToCivilizations(IEnumerable<XElement> civilizationElements)
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

    private void AssignLeadersToCivilizations(ref List<Civilization> civilizations, ref List<InfluentialPerson> influentialPeople)
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

    private void GenerateLeaderUnits(ref List<Civilization> civilizations, ref List<Holding> holdings)
    {
        foreach (Civilization c in civilizations)
        {
            foreach (InfluentialPerson ip in c.InfluentialPeople)
            {
                if (ip.IsLeader)
                {
                    Unit leaderUnit = new Unit(Guid.NewGuid().ToString().ToLower(), ip.DisplayName,UnitType.Leader,ip);
                    c.Units.Add(leaderUnit);
                    this.AssignRandomStartingLocation(leaderUnit, c);
                }
            }
        }
    }

    private void AssignRandomStartingLocation(Unit unit, Civilization civlization)
    {
        Holding tempHolding = this.spawnHoldings[0];
        tempHolding.Unit = unit;
        tempHolding.DiscoveredCivilizationGUIDs.Add(civlization.GUID);
        //civlization.DiscoveredHoldingGUIDs.Add(tempHolding.GUID);
        this.spawnHoldings.RemoveAt(0);
    }

    private void AssignAdjacentHoldings(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            h.AdjacentHoldingGUIDs.AddRange(allHoldings.Where(ah=>
                (ah.XPosition == (h.XPosition + 1) && ah.ZPosition == h.ZPosition) ||
                (ah.XPosition == h.XPosition && ah.ZPosition == (h.ZPosition + 1)) ||
                (ah.XPosition == (h.XPosition - 1) && ah.ZPosition == h.ZPosition) ||
                (ah.XPosition == h.XPosition && ah.ZPosition == (h.ZPosition - 1))
            ).Select(x=>x.GUID).ToList());
        }
    }

    private XDocument GetXMLFile(string filePath)
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(filePath);
        var doc = XDocument.Parse(txtAsset.text);
        return doc;
    }
}
