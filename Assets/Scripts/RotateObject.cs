using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 90f; // Rotation speed in degrees per second
    [SerializeField]
    private Axis rotationAxis = Axis.Y; // Axis of rotation (default is Y)

    // Enum to define available rotation axes
    private enum Axis
    {
        X,
        Y,
        Z
    }

    private void Update()
    {
        // Define the rotation axis based on the selected enum value
        Vector3 axisVector = Vector3.zero;

        switch (rotationAxis)
        {
            case Axis.X:
                axisVector = Vector3.right;
                break;
            case Axis.Y:
                axisVector = Vector3.up;
                break;
            case Axis.Z:
                axisVector = Vector3.forward;
                break;
        }

        // Rotate the object around the specified axis at the specified speed
        transform.Rotate(axisVector * rotationSpeed * Time.deltaTime);
    }
}
