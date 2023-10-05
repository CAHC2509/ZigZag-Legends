using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    private GameObject coinParticlesPrefab;
    private ParticleSystem coinParticleSystem;

    private void Start()
    {
        coinParticlesPrefab = SingletonManager.WorldObjects.coinParticlesPrefab;
        coinParticleSystem = SingletonManager.WorldObjects.coinParticleSystem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SingletonManager.Managers.pointsManager.AddPoints();

            coinParticlesPrefab.transform.position = transform.position;

            if (coinParticleSystem.isPlaying)
                coinParticleSystem.Stop();

            coinParticleSystem.Play();

            Destroy(gameObject);
        }
    }
}
