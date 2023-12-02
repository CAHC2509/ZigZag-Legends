using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlatformController : MonoBehaviour
{
    [Header("Generation points settings")]
    [SerializeField]
    private Transform frontGenerationPoint;
    [SerializeField]
    private Transform rightGenerationPoint;
    [SerializeField]
    private List<Transform> generationPoints;

    [Space, Header("Main settings")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GameObject backBlock;
    [SerializeField]
    private float coinYOffset = 3f;
    [SerializeField]
    private float maxPlayerDistance = 5f;
    [SerializeField]
    private bool isStartingPlatform = false;
    [SerializeField]
    private bool isLastStartingPlatform = false;

    private GameObject coin = null;
    private Transform playerTransform;
    private float distanteToPlayer;
    private bool canFall = false;
    private bool hasFallen = false;

    private void Awake()
    {
        if (isStartingPlatform)
            SingleInstanceManager.WorldObjects.instanciatedPlatformControllers.Add(this);
    }

    private void Start()
    {
        if (isLastStartingPlatform)
        {
            SingleInstanceManager.WorldObjects.lastPlatformController = this;
            GenerateNextPlatform();
        }
    }

    private void Update()
    {
        if (transform.localPosition.y < SingleInstanceManager.WorldObjects.platformFallHeight)
            Destroy(gameObject);

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

        if (coin != null)
            coin.GetComponent<Rigidbody>().isKinematic = false;

        SingleInstanceManager.WorldObjects.instanciatedPlatforms.Remove(gameObject);
        SingleInstanceManager.WorldObjects.instanciatedPlatformControllers.Remove(this);
        
        ScoreManager.instance.UpdateCurrentScore();

        backBlock.SetActive(false); // Disable back block for a better effect

        hasFallen = true;
    }

    /// <summary>
    /// Generate the next platform on a random spawnpoint
    /// </summary>
    public void GenerateNextPlatform()
    {
        // Generate the next platform in a random place (spawnpoint)
        int randomIndex = Random.Range(0, generationPoints.Count);
        Transform generationPointSelected = generationPoints[randomIndex];

        GameObject platformPrefabSelected = null;

        if (generationPointSelected == frontGenerationPoint)
            platformPrefabSelected = SingleInstanceManager.WorldObjects.frontPlatformPrefab;
        else
            platformPrefabSelected = SingleInstanceManager.WorldObjects.rightPlatformPrefab;


        GameObject instanciatedPlatform = Instantiate(platformPrefabSelected, generationPointSelected.position, platformPrefabSelected.transform.rotation, null);
        SingleInstanceManager.WorldObjects.instanciatedPlatforms.Add(instanciatedPlatform);

        // Set the previous spawned platform as last platform controller
        PlatformController instanciatedPlatformController = instanciatedPlatform.GetComponent<PlatformController>();
        SingleInstanceManager.WorldObjects.instanciatedPlatformControllers.Add(instanciatedPlatformController);
        SingleInstanceManager.WorldObjects.lastPlatformController = instanciatedPlatformController;
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
        coin = Instantiate(SingleInstanceManager.WorldObjects.coinPrefab, spawnPosition, Quaternion.identity);
    }

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

    private void OnDestroy() => Destroy(coin);
}
