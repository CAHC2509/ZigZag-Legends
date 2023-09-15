using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SingletonManager
{
    public static class WorldObjects
    {        
        public static PlatformController lastPlatformController;
        public static GameObject platformPrefab;
        public static float platformFallHeight;

        public static List<GameObject> instanciatedPlatforms = new List<GameObject>();
    }

    public static class PointsSystem
    {
        public static PointsManager pointsManager;
        public static int matchPoints;
    }
}
