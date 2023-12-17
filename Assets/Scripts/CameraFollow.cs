using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField]
    private Vector3 playerOffset = new Vector3(5f, 7f, 5f); // Offset from the target's position.

    [Space, Header("Menu settings")]
    [SerializeField]
    private Vector3 menuPosition = new Vector3(5f, 125f, 5f);
    [SerializeField]
    private Vector3 carSelectionPosition = new Vector3(5f, 83f, 5f);
    [SerializeField]
    private Quaternion menuRotation = Quaternion.Euler(40f, -135f, 0f);

    private Vector3 offset = Vector3.zero;
    private Transform target = null;

    private void LateUpdate()
    {
        if (target != null && offset == playerOffset)
        {
            // Calculate the desired camera position based on the target's position and offset.
            Vector3 desiredPosition = target.position + offset;

            // Adjust the interpolation speed based on your preferences.
            float interpolationSpeed = 10f;

            // Smoothly move the camera towards the desired position.
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * interpolationSpeed);

            // Make the camera look at the target only if necessary.
            if (Vector3.Distance(transform.position, desiredPosition) > 0.001f)
                transform.LookAt(target);
        }
    }

    /// <summary>
    /// Focus the camera on a new target
    /// </summary>
    /// <param name="targetType">0 for find player, 1 for find menu</param>
    public void FindNewTarget(int targetType)
    {
        switch (targetType)
        {
            case 0:
                target = SingleInstanceManager.Player.playerController.transform;
                offset = playerOffset;
                break;

            case 1:
                target = null;
                transform.position = carSelectionPosition;
                transform.rotation = menuRotation;
                break;

            case 2:
                target = null;
                transform.position = menuPosition;
                transform.rotation = menuRotation;
                break;
        }
    }

    public void StopFollowingTarget() => target = null;
}
