using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationManager : MonoBehaviour
{
    [SerializeField]
    private int maxPlatformsCount = 25;
    [SerializeField]
    private PlatformController startPlatform;
    [SerializeField]
    private GameObject platformPrefab;
    [SerializeField]
    private float platformFallHeight = -25f;

    private void Awake()
    {
        SingletonManager.WorldObjects.lastPlatformController = startPlatform;
        SingletonManager.WorldObjects.platformPrefab = platformPrefab;
        SingletonManager.WorldObjects.platformFallHeight = platformFallHeight;

        SingletonManager.WorldObjects.instanciatedPlatforms.Clear();
    }

    private void Update()
    {
        if (SingletonManager.WorldObjects.instanciatedPlatforms.Count < maxPlatformsCount)
            SingletonManager.WorldObjects.lastPlatformController.GenerateNextPlatform();
    }
}
