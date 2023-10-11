using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationManager : MonoBehaviour
{
    [Header("Platforms settings")]
    [SerializeField]
    private int maxPlatformsCount = 25;
    [SerializeField]
    private int specialPlatformSpawnRate = 25;
    [SerializeField]
    private float platformFallHeight = -25f;
    [SerializeField]
    private GameObject platformPrefab;
    [SerializeField]
    private GameObject initialPlatformsPrefab;

    [Space, Header("Coins settings")]
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject coinParticlesPrefab;

    private int generatedPlatformsCount = 0;  // Counter for generated platforms

    private void Awake()
    {
        // Initialize platform references and clear the list of instantiated platforms
        SingletonManager.WorldObjects.platformPrefab = platformPrefab;
        SingletonManager.WorldObjects.platformFallHeight = platformFallHeight;
        SingletonManager.WorldObjects.instanciatedPlatforms.Clear();
        SingletonManager.WorldObjects.instanciatedPlatformControllers.Clear();

        // Initialize coin and particles references
        GameObject particles = Instantiate(coinParticlesPrefab);
        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();

        SingletonManager.WorldObjects.coinParticlesPrefab = particles;
        SingletonManager.WorldObjects.coinParticleSystem = particleSystem;
        SingletonManager.WorldObjects.coinPrefab = coinPrefab;
    }

    private void Update()
    {
        // Cache the conditions to improve performance and readability
        bool shouldGenerateNewPlatform = SingletonManager.WorldObjects.instanciatedPlatforms.Count < maxPlatformsCount;
        bool lastPlatformControllerIsNull = SingletonManager.WorldObjects.lastPlatformController == null;

        if (shouldGenerateNewPlatform && !lastPlatformControllerIsNull)
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

    /// <summary>
    /// Spawn the initial corridor of platforms
    /// </summary>
    public void SpawnInitialPlatforms() => Instantiate(initialPlatformsPrefab);

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
        SingletonManager.WorldObjects.lastPlatformController = null;
        SingletonManager.WorldObjects.instanciatedPlatforms.Clear();
        SingletonManager.WorldObjects.instanciatedPlatformControllers.Clear();

        generatedPlatformsCount = 0;
    }
}
