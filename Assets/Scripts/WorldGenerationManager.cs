using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationManager : MonoBehaviour
{
    [SerializeField]
    private int maxPlatformsCount = 25;
    [SerializeField]
    private int specialPlatformSpawnRate = 25;
    [SerializeField]
    private float platformFallHeight = -25f;
    [SerializeField]
    private PlatformController startPlatform;
    [SerializeField]
    private GameObject platformPrefab;
    [SerializeField]
    private GameObject specialPlatformPrefab;

    private int generatedPlatformsCount = 0;  // Counter for generated platforms

    private void Awake()
    {
        // Initialize references and clear the list of instantiated platforms
        SingletonManager.WorldObjects.lastPlatformController = startPlatform;
        SingletonManager.WorldObjects.platformPrefab = platformPrefab;
        SingletonManager.WorldObjects.specialPlatformPrefab = specialPlatformPrefab;
        SingletonManager.WorldObjects.platformFallHeight = platformFallHeight;
        SingletonManager.WorldObjects.instanciatedPlatforms.Clear();
    }

    private void Update()
    {
        if (SingletonManager.WorldObjects.instanciatedPlatforms.Count < maxPlatformsCount)
        {
            // Check if we've generated enough platforms and spawn a special one
            if (generatedPlatformsCount >= specialPlatformSpawnRate)
            {
                // Generate a special platform
                SingletonManager.WorldObjects.lastPlatformController.GenerateNextPlatform(true);
                generatedPlatformsCount = 0;
            }
            else
            {
                // Generate a normal platform
                SingletonManager.WorldObjects.lastPlatformController.GenerateNextPlatform();
            }

            generatedPlatformsCount++;  // Increment the counter
        }
    }
}
