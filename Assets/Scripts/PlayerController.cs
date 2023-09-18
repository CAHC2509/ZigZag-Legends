using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float fallHeight = -5f;

    [Space, Header("Main settings")]
    [SerializeField]
    private float speedIncreaseValue = 0.1f;
    [SerializeField]
    private float speedIncreaseInterval = 10f;

    private bool isMoving = false;
    private bool isMovingLeft = false;
    private bool hasDetectedTouch = false;
    private float startTime;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        // Record the time when the game started.
        startTime = Time.time;
    }

    private void Update()
    {

#if UNITY_ANDROID
        // Detect player input (touch).
        if (Input.touchCount > 0 && !hasDetectedTouch)
            InputRecognized();

        if (Input.touchCount == 0)
            hasDetectedTouch = false;
#endif

#if UNITY_EDITOR
        // Detect player input (click).
        if (Input.GetMouseButtonDown(0))
            InputRecognized();
#endif

        if (transform.localPosition.y < fallHeight)
            Death();

        // Calculate elapsed time since the game started.
        float elapsedTime = Time.time - startTime;

        // Increase speed every 'speedIncreaseInterval' seconds after the player has started the game.
        if (isMoving && elapsedTime >= speedIncreaseInterval)
        {
            moveSpeed += speedIncreaseValue;
            startTime = Time.time; // Reset the time counter.
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            // Move the player forward (z) at a constant speed.
            Vector3 forwardMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }
    }

    private void InputRecognized()
    {
        hasDetectedTouch = true;

        if (isMoving)
            ChangeDirection();
        else
            isMoving = true;
    }

    // Change the player's direction.
    private void ChangeDirection()
    {
        isMovingLeft = !isMovingLeft;
        Vector3 newDirection = isMovingLeft ? Vector3.left : Vector3.back;

        // Rotate the player to face the new direction.
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void Death()
    {
        int actualPoints = SingletonManager.PointsSystem.matchPoints;
        PlayerPrefsUtility.AddPlayerPoints(actualPoints);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
