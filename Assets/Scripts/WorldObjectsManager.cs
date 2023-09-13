using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldObjectsManager
{
    public static GameObject platformPrefab;
    public static float platformFallHeight;

    public static PlatformController lastPlatformController;

    public static List<GameObject> instanciatedPlatforms = new List<GameObject>();
}
