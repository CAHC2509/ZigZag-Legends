using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(5f, 5f, 5f); // Offset from the target's position.

    private Transform target;

    private void Awake()
    {
        SingletonManager.Player.cameraFollow = this;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired camera position based on the target's position and offset.
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the desired position.
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

            // Make the camera look at the target.
            transform.LookAt(target);
        }
    }

    public void FindPlayer() => target = GameObject.FindGameObjectWithTag("Player").transform;
}
