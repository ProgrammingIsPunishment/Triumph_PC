using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class SaveController : MonoBehaviour
{
    public Save NewGame(string mapName)
    {
        Tuple<List<ResourceItem>, List<Holding>, List<InfluentialPerson>> mapData = this.LoadMapFile(mapName);
        return new Save("New Game",mapName,mapData.Item2, mapData.Item1, mapData.Item3);
    }

    public void Save(Save save)
    {
        using (Stream stream = File.Open(Application.persistentDataPath + "/" + save.FileName(), FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, save);
        }
    }

    public Save Load(string saveFileName)
    {
        string path = Application.persistentDataPath + "/" + saveFileName + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save save = formatter.Deserialize(stream) as Save;
            stream.Close();

            return save;
        }
        else
        {
            Debug.Log($"{saveFileName} not Found in {Application.persistentDataPath}");
            return null;
        }
    }

    private Tuple<List<ResourceItem>,List<Holding>, List<InfluentialPerson>> LoadMapFile(string mapName)
    {
        Tuple<List<ResourceItem>, List<Holding>, List<InfluentialPerson>> result = null;
        XDocument doc = this.GetXMLFile("Maps/" + mapName);

        var allResourceItems = doc.Element("map").Elements("resourceitems").Elements("resourceitem");
        var allInfluentialPeople = doc.Element("map").Elements("influentialpeople").Elements("influentialperson");
        var allHoldings = doc.Element("map").Elements("holdings").Elements("holding");

        List<ResourceItem> workingResourceItems = new List<ResourceItem>();
        List<InfluentialPerson> workingInfluentialPeople = new List<InfluentialPerson>();
        List<Holding> workingHoldings = new List<Holding>();

        //Loop through all the resource items
        foreach (var ri in allResourceItems)
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

                resourceItemComponents.Add(new Tuple<string, int>(ricGuid,amount));
            }

            workingResourceItems.Add(new ResourceItem(guid,displayName,resourceItemType,stackLimit,resourceItemComponents));
        }

        //Loop through all influential people
        foreach (var ip in allInfluentialPeople)
        {
            string guid = (string)ip.Attribute("guid").Value.ToLower();
            string displayName = (string)ip.Attribute("displayname").Value;

            workingInfluentialPeople.Add(new InfluentialPerson(guid, displayName));
        }

        //Loop through all the holdings
        foreach (var h in allHoldings)
        {
            string guid = Guid.NewGuid().ToString().ToLower();
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

                ResourceItem tempResourceItem = workingResourceItems.Find(wri => wri.GUID.ToLower() == riGuid.ToLower()).CreateInstance();
                tempResourceItem.AddToStack(amount);

                resourceItems.Add(tempResourceItem);
            }

            Holding tempHolding = new Holding(name, xPosition, zPosition, terrainType, resourceItems);
            tempHolding.GUID = guid;
            workingHoldings.Add(tempHolding);
        }

        result = new Tuple<List<ResourceItem>, List<Holding>, List<InfluentialPerson>>(workingResourceItems,workingHoldings,workingInfluentialPeople);

        return result;
    }

    private XDocument GetXMLFile(string filePath)
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(filePath);
        var doc = XDocument.Parse(txtAsset.text);
        return doc;
    }
}
