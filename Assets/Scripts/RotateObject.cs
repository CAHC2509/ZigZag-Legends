using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 90f; // Rotation speed in degrees per second

    private void Update()
    {
        // Rotate the object around the Y-axis at the specified speed
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
