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
        WorldObjectsManager.lastPlatformController = startPlatform;
        WorldObjectsManager.platformPrefab = platformPrefab;
        WorldObjectsManager.platformFallHeight = platformFallHeight;

        WorldObjectsManager.instanciatedPlatforms.Clear();
    }

    private void Update()
    {
        if (WorldObjectsManager.instanciatedPlatforms.Count < maxPlatformsCount)
            WorldObjectsManager.lastPlatformController.GenerateNextPlatform();
    }
}
