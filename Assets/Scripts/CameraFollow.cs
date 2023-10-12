using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField]
    private Vector3 playerOffset = new Vector3(5f, 7f, 5f); // Offset from the target's position.

    [Space, Header("Menu settings")]
    [SerializeField]
    private Vector3 menuPosition = new Vector3(5f, 33f, 5f);
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

            // Smoothly move the camera towards the desired position.
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

            // Make the camera look at the target.
            transform.LookAt(target);
        }
    }

    /// <summary>
    /// Focus the camera on a new target
    /// </summary>
    /// <param name="targetType">0 for find player, 1 for find menu</param>
    public void FindNewTarget(int targetType)
    {
        if (targetType == 0)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            offset = playerOffset;
        }
        else if (targetType == 1)
        {
            target = null;
            transform.position = menuPosition;
            transform.rotation = menuRotation;
        }
    }

    public void StopFollowingTarget() => target = null;
}
