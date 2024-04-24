using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Oberkommando
{
    public static SaveController SAVE_CONTROLLER = new SaveController();
    public static MainMenuController MAINMENU_CONTROLLER = new MainMenuController();
    public static MapController MAP_CONTROLLER = new MapController();
    public static TurnController TURN_CONTROLLER = new TurnController();

    public static InitializationController INITIALIZATION_CONTROLLER;
    public static PrefabController PREFAB_CONTROLLER;
    public static GameController GAME_CONTROLLER;
    public static UIController UI_CONTROLLER;
    public static ColdStorageController COLDSTORAGE_CONTROLLER;

    public static CameraManager CAMERA_MANAGER;

    public static Save SAVE;

    public static Holding SELECTED_HOLDING = null;
    public static Unit SELECTED_UNIT = null;
    public static int SELECTED_LOT = 0;

    public static void ClearSelections()
    {
        SELECTED_HOLDING = null;
        SELECTED_UNIT = null;
        SELECTED_LOT = 0;
    }
}
