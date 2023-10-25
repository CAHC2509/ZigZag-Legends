using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SingletonManager
{
    public static class Player
    {
        // Camera
        public static CameraFollow cameraFollow;
        public static CameraShake cameraShake;
        public static CameraBackgroundColorChanger cameraColorChanger;

        // Player
        public static PlayerController playerController;
    }

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
        public static List<PlatformController> instanciatedPlatformControllers = new List<PlatformController>();
    }

    public static class Managers
    {
        public static GameManager gameManager;
        public static HighScoreManager highScoreManager;
        public static PointsManager pointsManager;
        public static CarUnlockManager carUnlockManager;
    }
}
