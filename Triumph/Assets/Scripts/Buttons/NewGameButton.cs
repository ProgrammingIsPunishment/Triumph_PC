using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    public void ClickEvent()
    {
        this.ForTesting();
    }

    private void ForTesting()
    {
        Save result = new Save("New Game");
        result.MapName = "onyx";
        Oberkommando.SAVE = Oberkommando.SAVE_CONTROLLER.NewGame(result);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
