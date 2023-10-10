using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TireSmoke : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem tireParticles;
    [SerializeField]
    private float stopParticlesDelay = 0.25f;

    private bool isCollidingWithPlatform = false;
    private Coroutine stopParticlesCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            isCollidingWithPlatform = true;

            if (stopParticlesCoroutine != null)
            {
                StopCoroutine(stopParticlesCoroutine);
                stopParticlesCoroutine = null;
            }

            tireParticles.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            isCollidingWithPlatform = false;

            if (stopParticlesCoroutine == null)
                stopParticlesCoroutine = StartCoroutine(StopParticlesAfterDelay(stopParticlesDelay));
        }
    }

    private IEnumerator StopParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isCollidingWithPlatform)
            tireParticles.Stop();

        stopParticlesCoroutine = null;
    }
}
