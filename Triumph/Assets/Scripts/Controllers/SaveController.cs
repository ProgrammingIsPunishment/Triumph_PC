using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class SaveController
{
    public Save NewGame(Save save)
    {
        Tuple<List<ResourceItem>, List<Holding>, List<Civilization>> mapData = Oberkommando.MAP_CONTROLLER.LoadMapFile(save.MapName);

        save.AllResourceItems = mapData.Item1;
        save.AllHoldings = mapData.Item2;
        save.AllCivilizations = mapData.Item3;

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
