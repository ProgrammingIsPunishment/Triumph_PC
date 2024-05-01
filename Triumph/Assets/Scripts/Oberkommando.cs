using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Oberkommando
{
    public static MapService MAP_SERVICE = new MapService();
    public static PrefabsService PREFAB_SERVICE = new PrefabsService();

    public static GameInitializationController GAMEINITIALIZATION_CONTROLLER;
    public static DebugController DEBUG_CONTROLLER;

    public static CameraManager CAMERA_MANAGER;

    public static Save SAVE;
}
