using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    public void OnClickEvent()
    {
        Save tempSave = new Save("Obsidian save game","obsidian");
        Map tempMap = Oberkommando.MAP_SERVICE.LoadMap("obsidian");
        tempSave.Holdings = tempMap.Holdings;
        tempSave.Units = tempMap.Units;
        tempSave.Civilizations = tempMap.Civilizations;
        tempSave.PlayerGUID = "rome";

        Oberkommando.SAVE = tempSave;

        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
