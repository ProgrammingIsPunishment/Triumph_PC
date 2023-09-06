using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void Start()
    {
        Oberkommando.MAINMENU_CONTROLLER = this;
    }
}
