using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float coinYOffset = 3f;
    [SerializeField]
    private float maxPlayerDistance = 5f;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private List<Transform> generationPoints;
    [SerializeField]
    private bool isStartingPlatform = false;
    [SerializeField]
    private bool isLastStartingPlatform = false;

    private Transform playerTransform;
    private float distanteToPlayer;
    private bool canFall = false;
    private bool hasFallen = false;

    private void Awake()
    {
        if (isStartingPlatform)
            SingletonManager.WorldObjects.instanciatedPlatformControllers.Add(this);
    }

    private void Start()
    {
        if (isLastStartingPlatform)
        {
            SingletonManager.WorldObjects.lastPlatformController = this;
            GenerateNextPlatform();
        }
    }

    private void Update()
    {
        if (transform.localPosition.y < SingletonManager.WorldObjects.platformFallHeight)
            DestroyThisPlatform();

        if (playerTransform != null)
        {
            distanteToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            if (distanteToPlayer > maxPlayerDistance && canFall && !hasFallen)
                MakePlatformFall();
        }
    }

    /// <summary>
    /// Make the platform fall by disable it's rigidbody kinematic state
    /// </summary>
    private void MakePlatformFall()
    {
        rb.isKinematic = false;

        SingletonManager.WorldObjects.instanciatedPlatforms.Remove(gameObject);
        SingletonManager.WorldObjects.instanciatedPlatformControllers.Remove(this);
        SingletonManager.Managers.highScoreManager.UpdateActualScore();

        hasFallen = true;
    }

    /// <summary>
    /// Generate the next platform on a random spawnpoint
    /// </summary>
    public void GenerateNextPlatform()
    {
        // Cache the platform prefab
        GameObject platform = SingletonManager.WorldObjects.platformPrefab;

        // Generate the next platform in a random place (spawnpoint)
        int randomIndex = Random.Range(0, generationPoints.Count);
        GameObject instanciatedPlatform = Instantiate(platform, generationPoints[randomIndex].position, Quaternion.identity, null);
        SingletonManager.WorldObjects.instanciatedPlatforms.Add(instanciatedPlatform);

        // Set the previous spawned platform as last platform controller
        PlatformController instanciatedPlatformController = instanciatedPlatform.GetComponent<PlatformController>();
        SingletonManager.WorldObjects.instanciatedPlatformControllers.Add(instanciatedPlatformController);
        SingletonManager.WorldObjects.lastPlatformController = instanciatedPlatformController;
    }

    /// <summary>
    /// Spawns a coin on top of the platform
    /// </summary>
    public void SpawnCoin()
    {
        // Set the offset to spawn the coin
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += coinYOffset;

        // Spawn coin and set as child of the current platform
        GameObject coin = Instantiate(SingletonManager.WorldObjects.coinPrefab, spawnPosition, Quaternion.identity);
        coin.transform.SetParent(transform);
    }

    private void DestroyThisPlatform() => Destroy(gameObject);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerTransform = collision.transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            canFall = true;
    }
}
