using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SingleInstanceManager
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
        // Explossion
        public static GameObject currentExplossion;

        // Coins
        public static GameObject coinPrefab;

        // Platforms
        public static GameObject frontPlatformPrefab;
        public static GameObject rightPlatformPrefab;
        public static PlatformController lastPlatformController;
        public static float platformFallHeight;
        public static List<GameObject> instanciatedPlatforms = new List<GameObject>();
        public static List<PlatformController> instanciatedPlatformControllers = new List<PlatformController>();
    }

    public static class Managers
    {
        public static GameManager gameManager;
        public static PointsManager pointsManager;
        public static CarUnlockManager carUnlockManager;
    }
}
