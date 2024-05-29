using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Oberkommando
{
    public static MapService MAP_SERVICE = new MapService();
    public static PrefabsService PREFAB_SERVICE = new PrefabsService();
    public static UtilitiesService UTILITIES_SERVICE = new UtilitiesService();
    public static AIService AI_SERVICE = new AIService();

    public static GameInitializationController GAMEINITIALIZATION_CONTROLLER;
    public static GameController GAME_CONTROLLER;
    public static UIController UI_CONTROLLER;
    public static DispatchesController DISPATCHES_CONTROLLER;

    public static CameraManager CAMERA_MANAGER;

    public static SyntaxLibrary SYNTAXLIBRARY;
    public static ValueLibrary VALUELIBRARY;

    public static Save SAVE;
    public static Civilization PLAYER;

    public static bool ISDEBUGMODE;
}
