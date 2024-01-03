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
    private Dictionary<string, Tuple<string, int>> heatDictionary = new Dictionary<string, Tuple<string, int>>();
    private Dictionary<string, string> resourceDictionary = new Dictionary<string, string>();
    private Dictionary<Tuple<int,int>, Tuple<string, string>> holdingDictionary = new Dictionary<Tuple<int, int>, Tuple<string, string>>();
    private Dictionary<string, string> spawnDictionary = new Dictionary<string, string>();

    public Tuple<List<ResourceItem>, List<Holding>, List<Civilization>, List<Unit>> LoadMapFile(string mapName)
    {
        Tuple<List<ResourceItem>, List<Holding>, List<Civilization>, List<Unit>> result = null;
        XDocument doc = this.GetXMLFile($"Maps/{mapName}/{mapName}_manifest");

        var allSpawnDefinitionElements = doc.Element("map").Elements("spawns").Elements("spawn");
        var allHeatDefinitionElements = doc.Element("map").Elements("heats").Elements("heat");
        var allTerrainDefinitionElements = doc.Element("map").Elements("terrains").Elements("terrain");

        var allResourceItemElements = doc.Element("map").Elements("resourceitems").Elements("resourceitem");
        var allInfluentialPeopleElements = doc.Element("map").Elements("influentialpeople").Elements("influentialperson");
        var allBuildingElements = doc.Element("map").Elements("buildings").Elements("building");
        var allCivilizationsElements = doc.Element("map").Elements("civilizations").Elements("civilization");
        var allHoldingsElements = doc.Element("map").Elements("holdings").Elements("holding");

        List<ResourceItem> workingResourceItems = new List<ResourceItem>();
        List<Holding> workingHoldings = new List<Holding>();
        List<Unit> workingUnits = new List<Unit>();
        List<Civilization> workingCivilizations = new List<Civilization>();

        List<InfluentialPerson> workingInfluencialPeople = new List<InfluentialPerson>();
        List<Building> workingBuildings = new List<Building>();

        //Generate independent definition dictionaries
        this.heatDictionary = this.ConvertToHeatDictionary(allHeatDefinitionElements);
        this.terrainDictionary = this.ConvertToTerrainDictionary(allTerrainDefinitionElements);
        this.holdingDictionary = this.ConvertToHoldingDictionary(allHoldingsElements);
        this.spawnDictionary = this.ConvertToSpawnDictionary(allSpawnDefinitionElements);

        //Loop through all the resource items
        workingResourceItems.AddRange(this.ConvertToResourceItems(allResourceItemElements));

        //Loop through all influential people
        workingInfluencialPeople.AddRange(this.ConvertToInfluentialPeople(allInfluentialPeopleElements));

        //Loop through all buildings
        workingBuildings.AddRange(this.ConvertToBuildings(allBuildingElements));

        //Loop through all the holdings
        workingHoldings.AddRange(this.ReadMapTextures(mapName, workingResourceItems));

        //Loop through all the civilizations...generate units for the civilization as well
        workingCivilizations.AddRange(this.ConvertToCivilizations(allCivilizationsElements, workingHoldings, workingInfluencialPeople, workingResourceItems, workingBuildings));

        //Loop through all units and add them to the complete list of units
        foreach (Civilization c in workingCivilizations) { workingUnits.AddRange(c.Units); }

        //this.AssignLeadersToCivilizations(ref workingCivilizations, ref tempInfluentialPeople);

        //this.GenerateLeaderUnits(ref workingCivilizations, ref workingHoldings, ref workingResourceItems);
        this.AssignAdjacentHoldings(workingHoldings);

        result = new Tuple<List<ResourceItem>, List<Holding>, List<Civilization>, List<Unit>>(workingResourceItems, workingHoldings, workingCivilizations, workingUnits);

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

    private Dictionary<string, Tuple<string, int>> ConvertToHeatDictionary(IEnumerable<XElement> heatDefinitionElements)
    {
        Dictionary<string, Tuple<string, int>> result = new Dictionary<string, Tuple<string, int>>();

        //Loop through all the terrains
        foreach (var hd in heatDefinitionElements)
        {
            int amount = int.Parse(hd.Attribute("amount").Value);
            string colorHexCode = (string)hd.Attribute("hexcode").Value;
            string guid = (string)hd.Attribute("guid").Value;

            result.Add(colorHexCode, new Tuple<string,int>(guid, amount));
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
        Texture2D resources2Texture = Resources.Load<Texture2D>($"Maps/{mapName}/{mapName}_resources_2");
        Texture2D spawnsTexture = Resources.Load<Texture2D>($"Maps/{mapName}/{mapName}_spawns");

        List<Holding> result = new List<Holding>();
        int width = terrainTexture.width;
        int height = terrainTexture.height;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                List<ResourceItem> workingNaturalResourceItems = new List<ResourceItem>();

                string terrainColorHex = ColorUtility.ToHtmlStringRGB(terrainTexture.GetPixel(x, z));
                string resources1ColorHex = ColorUtility.ToHtmlStringRGB(resources1Texture.GetPixel(x, z));
                string resources2ColorHex = ColorUtility.ToHtmlStringRGB(resources2Texture.GetPixel(x, z));
                //string resources1HeatColorHex = ColorUtility.ToHtmlStringRGB(resources1HeatTexture.GetPixel(x, z));
                string spawnColorHex = ColorUtility.ToHtmlStringRGB(spawnsTexture.GetPixel(x, z));

                bool foundHolding = this.holdingDictionary.TryGetValue(new Tuple<int,int>(x,z), out Tuple<string,string> guidAndDisplayName);
                bool foundTerrain = this.terrainDictionary.TryGetValue(terrainColorHex,out TerrainType terrainType);
                //bool foundResource1 = this.resourceDictionary.TryGetValue(resources1ColorHex, out string resource1GUID);
                //bool foundHeat1 = this.heatDictionary.TryGetValue(resources1HeatColorHex, out int amount1);
                bool foundResourceHeat1 = this.heatDictionary.TryGetValue(resources1ColorHex, out Tuple<string,int> resourceHeat1Tuple);
                bool foundResourceHeat2 = this.heatDictionary.TryGetValue(resources2ColorHex, out Tuple<string,int> resourceHeat2Tuple);
                bool foundSpawn = this.spawnDictionary.TryGetValue(spawnColorHex, out string spawnGroup);

                if (foundResourceHeat1 || foundResourceHeat2)
                {
                    List<ResourceItem> tempResourceItems = new List<ResourceItem>();

                    if (foundResourceHeat1) {
                        ResourceItem tempResourceItem1 = allResourceItems.Find(wri => wri.GUID.ToLower() == resourceHeat1Tuple.Item1.ToLower()).CreateInstance();
                        tempResourceItem1.AddToStack(resourceHeat1Tuple.Item2);
                        tempResourceItems.Add(tempResourceItem1);
                    }
                    
                    if (foundResourceHeat2)
                    {
                        ResourceItem tempResourceItem2 = allResourceItems.Find(wri => wri.GUID.ToLower() == resourceHeat2Tuple.Item1.ToLower()).CreateInstance();
                        tempResourceItem2.AddToStack(resourceHeat2Tuple.Item2);
                        tempResourceItems.Add(tempResourceItem2);
                    }

                    foreach (ResourceItem ri in tempResourceItems)
                    {
                        switch (ri.ResourceItemType)
                        {
                            case ResourceItemType.Foliage:
                                workingNaturalResourceItems.Add(ri);
                                break;
                            case ResourceItemType.Fauna:
                                workingNaturalResourceItems.Add(ri);
                                break;
                            case ResourceItemType.Harvested:
                                break;
                            case ResourceItemType.Manufactured:
                                break;
                            default:
                                break;
                        }
                    }
                }

                Inventory naturalResourcesInventory = new Inventory(InventoryType.NaturalResources,workingNaturalResourceItems);

                Holding tempHolding = new Holding(guidAndDisplayName.Item2,x,z,terrainType, naturalResourcesInventory);
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
            string iconFileName = (string)ri.Attribute("iconFileName").Value.ToLower();
            string modelfilename = (string)ri.Attribute("modelfilename").Value.ToLower();
            List<Tuple<string, int>> resourceItemComponents = new List<Tuple<string, int>>();

            //string hexcode = (string)ri.Attribute("hexcode").Value;
            //if (hexcode != "null") { this.resourceDictionary.Add(hexcode, guid); }

            //loop through resource item components
            var allComponents = ri.Elements("resourceitemcomponents").Elements("resourceitemcomponent");
            foreach (var ric in allComponents)
            {
                string ricGuid = (string)ric.Attribute("guid").Value;
                int amount = int.Parse(ric.Attribute("amount").Value);

                resourceItemComponents.Add(new Tuple<string, int>(ricGuid, amount));
            }
            result.Add(new ResourceItem(guid, displayName, iconFileName, resourceItemType, stackLimit, resourceItemComponents,modelfilename));
        }

        return result;
    }

    private List<Supply> ConvertToSupplies(IEnumerable<XElement> supplyElements)
    {
        List<Supply> result = new List<Supply>();

        //Loop through all the resource items
        foreach (var s in supplyElements)
        {
            string guid = (string)s.Attribute("guid").Value.ToLower();
            string displayName = (string)s.Attribute("displayname").Value;

            //loop through attritions
            var allAttritions = s.Elements("attritions").Elements("attrition");
            List<Attrition> workingAttritions = new List<Attrition>();
            foreach (var a in allAttritions)
            {
                string resourceGUID = (string)a.Attribute("resourceguid").Value.ToLower();
                int maximum = int.Parse(a.Attribute("maximum").Value);
                int perTurnLoss = int.Parse(a.Attribute("perturnloss").Value);

                workingAttritions.Add(new Attrition(resourceGUID,maximum,perTurnLoss));
            }

            result.Add(new Supply(guid,displayName,workingAttritions));
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

    private List<Building> ConvertToBuildings(IEnumerable<XElement> buildingElements)
    {
        List<Building> result = new List<Building>();

        //Loop through all influential people
        foreach (var b in buildingElements)
        {
            string guid = (string)b.Attribute("guid").Value.ToLower();
            string displayName = (string)b.Attribute("displayname").Value;
            LayoutSize layoutSize = Enum.Parse<LayoutSize>(b.Attribute("layoutsize").Value);
            string iconFileName = (string)b.Attribute("iconfilename").Value;
            string modelFileName = (string)b.Attribute("modelfilename").Value;

            result.Add(new Building(guid, displayName, layoutSize, iconFileName, modelFileName));
        }

        return result;
    }

    private List<Civilization> ConvertToCivilizations(IEnumerable<XElement> civilizationElements, List<Holding> allHoldings, List<InfluentialPerson> allInfluentialPeople, List<ResourceItem> allResourceItems, List<Building> allBuildings)
    {
        List<Civilization> result = new List<Civilization>();

        //Loop through all influential people
        foreach (var c in civilizationElements)
        {
            string guid = (string)c.Attribute("guid").Value.ToLower();
            string displayName = (string)c.Attribute("displayname").Value;
            string startingLocationGUID = (string)c.Attribute("startinglocationguid").Value;
            string leaderGUID = (string)c.Attribute("leaderguid").Value;

            Civilization workingCivilization = new Civilization(guid, displayName);
            workingCivilization.ExploredHoldings.Add(allHoldings.Find(h=>h.GUID == startingLocationGUID));

            InfluentialPerson workingLeader = allInfluentialPeople.Find(ip => ip.GUID == leaderGUID);
            workingLeader.IsLeader = true;

            var allSupplies = c.Element("supplies").Elements("supply");
            List<Supply> workingSupplies = this.ConvertToSupplies(allSupplies);

            //Generate Units
            var allUnits = c.Element("units").Elements("unit");
            List<Unit> workingUnits = this.ConvertToUnits(allUnits,allHoldings,allInfluentialPeople, allResourceItems, workingSupplies);

            //Generate buildings
            var buildingElements = c.Element("buildings").Elements("building");
            foreach (var b in buildingElements)
            {
                string buildingTemplateGUID = (string)b.Attribute("templateguid").Value.ToLower();
                string buildingDisplayName = (string)b.Attribute("displayname").Value;
                int buildingLot = int.Parse(b.Attribute("lot").Value);
                string buildingLocationGUID = (string)b.Attribute("locationguid").Value;

                Building workingBuilding = allBuildings.Find(ab => ab.GUID == buildingTemplateGUID).CreateInstance(buildingLot);
                workingBuilding.DisplayName = buildingDisplayName;

                Holding workingHolding = allHoldings.Find(ab => ab.GUID == buildingLocationGUID);

                workingHolding.Buildings.Add(workingBuilding);
            }


            workingCivilization.InfluentialPeople.Add(workingLeader);
            workingCivilization.Leader = workingLeader;
            workingCivilization.Units = workingUnits;
            workingCivilization.Supplies = workingSupplies;
            result.Add(workingCivilization);
        }

        return result;
    }

    private List<Unit> ConvertToUnits(IEnumerable<XElement> unitElements, List<Holding> allHoldings, List<InfluentialPerson> allInfluentialPeople, List<ResourceItem> allResourceItems, List<Supply> civilizationSupplies)
    {
        List<Unit> result = new List<Unit>();

        //Loop through all the resource items
        foreach (var u in unitElements)
        {
            string guid = (string)u.Attribute("guid").Value.ToLower();
            string displayName = (string)u.Attribute("displayname").Value.ToLower();
            UnitType unitType = Enum.Parse<UnitType>(u.Attribute("unittype").Value);
            string commanderGUID = (string)u.Attribute("commanderguid").Value.ToLower();
            string startinglocationGUID = (string)u.Attribute("startinglocationguid").Value.ToLower();
            string supplyGUID = (string)u.Attribute("supplyguid").Value.ToLower();
            var inventoryResourceItems = u.Element("inventory").Elements("inventoryresourceitem");
            int actionPointLimit = int.Parse(u.Attribute("actionpointlimit").Value);

            Holding startingLocation = allHoldings.Find(h => h.GUID == startinglocationGUID);
            InfluentialPerson tempCommander = allInfluentialPeople.Find(ip => ip.GUID == commanderGUID);
            Supply tempSupply = civilizationSupplies.Find(cs=>cs.GUID == supplyGUID);

            //Generate inventory based on units inventory
            List<ResourceItem> workingResourceItems = new List<ResourceItem>();
            foreach (var iri in inventoryResourceItems)
            {
                string inventoryResourceItemGUID = (string)iri.Attribute("guid").Value.ToLower();
                int amount = int.Parse(iri.Attribute("amount").Value);

                ResourceItem tempResourceItem = allResourceItems.Find(ri => ri.GUID == inventoryResourceItemGUID).CreateInstance();
                tempResourceItem.AddToStack(amount);

                workingResourceItems.Add(tempResourceItem);
            }

            Inventory workingInventory = new Inventory(InventoryType.UnitSupply, workingResourceItems);

            Unit workingUnit = new Unit(displayName, startingLocation.XPosition, startingLocation.ZPosition, unitType, actionPointLimit);
            workingUnit.GUID = guid;
            workingUnit.Commander = tempCommander;
            workingUnit.Inventory = workingInventory;
            workingUnit.Supply = tempSupply;

            result.Add(workingUnit);
        }

        return result;
    }

    private void AssignAdjacentHoldings(List<Holding> allHoldings)
    {
        foreach (Holding h in allHoldings)
        {
            h.AdjacentHoldings.AddRange(allHoldings.Where(ah=>
                (ah.XPosition == (h.XPosition + 1) && ah.ZPosition == h.ZPosition) ||
                (ah.XPosition == h.XPosition && ah.ZPosition == (h.ZPosition + 1)) ||
                (ah.XPosition == (h.XPosition - 1) && ah.ZPosition == h.ZPosition) ||
                (ah.XPosition == h.XPosition && ah.ZPosition == (h.ZPosition - 1))
            ).Select(x=>x).ToList());
        }
    }

    private XDocument GetXMLFile(string filePath)
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(filePath);
        var doc = XDocument.Parse(txtAsset.text);
        return doc;
    }
}
