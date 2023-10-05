using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SingletonManager
{
    public static class WorldObjects
    {
        // Coins
        public static GameObject coinPrefab;
        public static GameObject coinParticlesPrefab;
        public static ParticleSystem coinParticleSystem;

        // Platforms
        public static PlatformController lastPlatformController;
        public static GameObject platformPrefab;
        public static float platformFallHeight;
        public static List<GameObject> instanciatedPlatforms = new List<GameObject>();
    }

    public static class Managers
    {
        public static HighScoreManager highScoreManager;
        public static PointsManager pointsManager;
    }
}
