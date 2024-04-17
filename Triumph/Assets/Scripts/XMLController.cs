using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class XMLController : MonoBehaviour
{
    public List<Tuple<string, Color32>> LoadMapFile(string mapName)
    {
        XDocument doc = this.GetXMLFile($"Maps/{mapName}/{mapName}");

        var allTerritoryElements = doc.Element("map").Elements("territories").Elements("territory");

        List<Tuple<string, Color32>> territories = new List<Tuple<string, Color32>>();

        foreach (var te in allTerritoryElements)
        {
            int colorR = int.Parse(te.Attribute("r").Value.ToLower());
            int colorG = int.Parse(te.Attribute("g").Value.ToLower());
            int colorB = int.Parse(te.Attribute("b").Value.ToLower());
            string name = (string)te.Attribute("name").Value.ToLower();

            Tuple<string, Color32> workingValue = new Tuple<string, Color32>(name,new Color32((byte)colorR, (byte)colorG, (byte)colorB, 255));
            territories.Add(workingValue);
        }

        return territories;
    }

    private XDocument GetXMLFile(string filePath)
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(filePath);
        var doc = XDocument.Parse(txtAsset.text);
        return doc;
    }
}
