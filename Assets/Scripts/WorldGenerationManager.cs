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
    private GameObject coinPrefab;

    private int generatedPlatformsCount = 0;  // Counter for generated platforms

    private void Awake()
    {
        // Initialize references and clear the list of instantiated platforms
        SingletonManager.WorldObjects.lastPlatformController = startPlatform;
        SingletonManager.WorldObjects.platformPrefab = platformPrefab;
        SingletonManager.WorldObjects.coinPrefab = coinPrefab;
        SingletonManager.WorldObjects.platformFallHeight = platformFallHeight;
        SingletonManager.WorldObjects.instanciatedPlatforms.Clear();
    }

    private void Update()
    {
        if (SingletonManager.WorldObjects.instanciatedPlatforms.Count < maxPlatformsCount)
        {
            SingletonManager.WorldObjects.lastPlatformController.GenerateNextPlatform();

            // Check if we've generated enough platforms and spawn a special one
            if (generatedPlatformsCount >= specialPlatformSpawnRate)
            {
                // Generate coin
                SingletonManager.WorldObjects.lastPlatformController.SpawnCoin();
                generatedPlatformsCount = 0;
            }

            generatedPlatformsCount++;  // Increment the counter
        }
    }
}
