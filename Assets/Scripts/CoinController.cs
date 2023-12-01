using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CoinController : MonoBehaviour
{
    [SerializeField]
    private Animator coinAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SingleInstanceManager.Managers.pointsManager.AddPoints();

            coinAnimator.SetTrigger("Collected");
        }
    }

    public void DestroyCoin() => Destroy(gameObject);
}
