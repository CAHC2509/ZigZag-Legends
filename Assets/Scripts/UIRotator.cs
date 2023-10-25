using UnityEngine;

public class UIRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 45.0f;
    [SerializeField]
    private RectTransform rectTransform;

    private void Start()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
    }

    private void Update() => rectTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
}
