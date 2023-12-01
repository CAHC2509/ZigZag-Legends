using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationManager : MonoBehaviour
{
    [Header("Platforms settings")]
    [SerializeField]
    private GameObject frontPlatformPrefab;
    [SerializeField]
    private GameObject rightPlatformPrefab;
    [SerializeField]
    private int maxPlatformsCount = 25;
    [SerializeField]
    private int specialPlatformSpawnRate = 25;
    [SerializeField]
    private float platformFallHeight = -25f;
    [SerializeField]
    private GameObject initialPlatformsPrefab;

    [Space, Header("Coins settings")]
    [SerializeField]
    private GameObject coinPrefab;

    private int generatedPlatformsCount = 0;  // Counter for generated platforms

    private void Awake()
    {
        // Initialize platform references and clear the list of instantiated platforms
        SingleInstanceManager.WorldObjects.frontPlatformPrefab = frontPlatformPrefab;
        SingleInstanceManager.WorldObjects.rightPlatformPrefab = rightPlatformPrefab;
        SingleInstanceManager.WorldObjects.platformFallHeight = platformFallHeight;
        SingleInstanceManager.WorldObjects.instanciatedPlatforms.Clear();
        SingleInstanceManager.WorldObjects.instanciatedPlatformControllers.Clear();

        // Initialize coin reference
        SingleInstanceManager.WorldObjects.coinPrefab = coinPrefab;
    }

    private void Update()
    {
        // Cache the conditions to improve performance and readability
        bool shouldGenerateNewPlatform = SingleInstanceManager.WorldObjects.instanciatedPlatforms.Count < maxPlatformsCount;
        bool lastPlatformControllerIsNull = SingleInstanceManager.WorldObjects.lastPlatformController == null;

        if (shouldGenerateNewPlatform && !lastPlatformControllerIsNull)
        {
            SingleInstanceManager.WorldObjects.lastPlatformController.GenerateNextPlatform();

            // Check if we've generated enough platforms and spawn a special one
            if (generatedPlatformsCount >= specialPlatformSpawnRate)
            {
                // Generate coin
                SingleInstanceManager.WorldObjects.lastPlatformController.SpawnCoin();
                generatedPlatformsCount = 0;
            }

            generatedPlatformsCount++;  // Increment the counter
        }
    }

    /// <summary>
    /// Destroy all the existing objects with tag "Platform"
    /// </summary>
    public void DestroyExistingPlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
            Destroy(platform);

        ResetWordGenerationValues();
    }

    /// <summary>
    /// Resets the generation values for prepare the next world generation
    /// </summary>
    private void ResetWordGenerationValues()
    {
        SingleInstanceManager.WorldObjects.lastPlatformController = null;
        SingleInstanceManager.WorldObjects.instanciatedPlatforms.Clear();
        SingleInstanceManager.WorldObjects.instanciatedPlatformControllers.Clear();

        generatedPlatformsCount = 0;
    }

    /// <summary>
    /// Spawn the initial corridor of platforms
    /// </summary>
    public void SpawnInitialPlatforms() => Instantiate(initialPlatformsPrefab);

    /// <summary>
    /// Destroy the last created explossion object
    /// </summary>
    public void DestroyCurrentExplosion() => Destroy(SingleInstanceManager.WorldObjects.currentExplossion);
}
