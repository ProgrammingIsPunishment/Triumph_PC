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

    public static CameraManager CAMERA_MANAGER;
    public static LeaderMovementManager LEADER_MOVEMENT_MANAGER = new LeaderMovementManager();

    public static List<Holding> SELECTED_HOLDINGS = new List<Holding>();

    public static UIState UISTATE;

    public static Save SAVE;
}
