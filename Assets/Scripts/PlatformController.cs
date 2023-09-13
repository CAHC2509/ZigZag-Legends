using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float maxPlayerDistance = 5f;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private List<Transform> generationPoints;


    private Transform playerTransform;
    private float distanteToPlayer;
    private bool canFall = false;
    private bool hasFallen = false;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (transform.localPosition.y < WorldObjectsManager.platformFallHeight)
            DestroyThisPlatform();

        distanteToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanteToPlayer > maxPlayerDistance && canFall && !hasFallen)
            MakePlatformFall();
    }

    private void MakePlatformFall()
    {
        rb.isKinematic = false;
        WorldObjectsManager.instanciatedPlatforms.Remove(gameObject);

        hasFallen = true;
    }

    public void GenerateNextPlatform()
    {
        int randomIndex = Random.Range(0, generationPoints.Count);
        GameObject instanciatedPlatform = Instantiate(WorldObjectsManager.platformPrefab, generationPoints[randomIndex].position, Quaternion.identity, null);
        WorldObjectsManager.instanciatedPlatforms.Add(instanciatedPlatform);

        WorldObjectsManager.lastPlatformController = instanciatedPlatform.GetComponent<PlatformController>();
    }

    private void DestroyThisPlatform()
    {
        Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            canFall = true;
    }
}
