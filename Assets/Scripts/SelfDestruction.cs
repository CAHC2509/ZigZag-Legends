using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 5f;

    private void Start() => Destroy(gameObject, delayTime);
}
