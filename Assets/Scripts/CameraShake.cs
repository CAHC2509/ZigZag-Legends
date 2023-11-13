using System.Collections;
using UnityEngine;

/// <summary>
/// This script needs to be attached to the camera to make the shake effect
/// </summary>
public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.5f;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Awake() => SingleInstanceManager.Player.cameraShake = this;

    /// <summary>
    /// Starts the camera shaking coroutine
    /// </summary>
    public void Shake()
    {
        originalPosition = transform.localPosition;

        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float timer = shakeDuration;

        while (timer > 0f)
        {
            ShakeCamera();
            timer -= Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    private void ShakeCamera()
    {
        float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
        float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
        transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);
    }

    /// <summary>
    /// Stops the camera shaking coroutine
    /// </summary>
    public void StopShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = originalPosition;
        }
    }
}
