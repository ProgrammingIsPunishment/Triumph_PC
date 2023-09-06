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
    public Save NewGame(Save save)
    {
        Tuple<List<ResourceItem>, List<Holding>, List<Civilization>> mapData = Oberkommando.MAP_CONTROLLER.LoadMapFile(save.MapName);

        ////FOR TESTING
        ////Create civilizations
        //List<Civilization> tempCivilizations = new List<Civilization>
        //{
        //    new Civilization("America", new List<string>(), new List<string>(), new List<string>())
        //};
        //save.AllCivilizations[0].DiscoveredHoldingGUIDs.Add(save.AllHoldings.Find(h => h.DisplayName == "greenwood").GUID);
        //save.AllCivilizations[0].InfluentialPeopleGUIDs.Add("abraham");
        //save.AllCivilizations[0].LeaderGUIDs.Add("abraham");
        //List<Unit> tempUnits = new List<Unit>
        //{
        //    new Unit(Guid.NewGuid().ToString().ToLower(),"Abraham",UnitType.Leader,"abraham",save.AllHoldings.Find(h => h.DisplayName == "greenwood").GUID)
        //};
        //save.AllUnits = tempUnits;
        //foreach (Unit u in save.AllUnits)
        //{
        //    save.AllHoldings.Find(h => h.GUID == u.HoldingGUID).Unit = u;
        //}
        ////END FOR TESTING

        return save;
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
}
