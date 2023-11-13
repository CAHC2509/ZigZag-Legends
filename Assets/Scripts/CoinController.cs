using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CoinController : MonoBehaviour
{
    private GameObject coinParticlesPrefab;
    private ParticleSystem coinParticleSystem;

    private void Start()
    {
        coinParticlesPrefab = SingleInstanceManager.WorldObjects.coinParticlesPrefab;
        coinParticleSystem = SingleInstanceManager.WorldObjects.coinParticleSystem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SingleInstanceManager.Managers.pointsManager.AddPoints();

            coinParticlesPrefab.transform.position = transform.position;

            if (coinParticleSystem.isPlaying)
                coinParticleSystem.Stop();

            coinParticleSystem.Play();

            Destroy(gameObject);
        }
    }
}
