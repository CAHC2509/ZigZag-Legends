using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float moveSpeed = 2.5f;
    [SerializeField]
    private float fallHeight = -3.5f;
    [SerializeField]
    private InputActionReference playerInputAction;

    [Space, Header("Speed settings")]
    [SerializeField]
    private float speedIncreaseValue = 0.25f;
    [SerializeField]
    private float speedIncreaseInterval = 7.5f;

    [Space, Header("Wheels settings")]
    [SerializeField]
    private List<RotateObject> wheels;
    [SerializeField]
    private List<GameObject> wheelsParticles;

    [Space, Header("Death settings")]
    [SerializeField]
    private GameObject explossionParticlesPrefab;

    private float startTime;
    private bool isMoving = false;
    private bool isMovingLeft = false;
    private bool hasFallen = false;

    private void OnEnable()
    {
        playerInputAction.action.Enable();
        playerInputAction.action.performed += PlayerInputDetected;
    }

    private void Awake() => SingleInstanceManager.Player.playerController = this;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        // Record the time when the game started.
        startTime = Time.time;
    }

    private void Update()
    {
        if (transform.localPosition.y < fallHeight && !hasFallen)
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

    private void PlayerInputDetected(InputAction.CallbackContext context)
    {
        if (isMoving)
        {
            ChangeDirection();
        }
        else
        {
            isMoving = true;
            SingleInstanceManager.Player.cameraColorChanger.ChangeColorChangeAllowedState(true);
            ChangeWheelsActiveState(true);
        }
    }

    private void ChangeDirection()
    {
        isMovingLeft = !isMovingLeft;
        Vector3 newDirection = isMovingLeft ? Vector3.left : Vector3.back;

        // Rotate the player to face the new direction.
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void ChangeWheelsActiveState(bool newState)
    {
        // Change wheels rotation enable state
        foreach (RotateObject wheel in wheels)
            wheel.enabled = newState;

        // Change wheels particles active state
        foreach (GameObject wheelParticles in wheelsParticles)
            wheelParticles.SetActive(newState);
    }

    private void Death()
    {
        hasFallen = true;

        SingleInstanceManager.Player.cameraShake.Shake();
        SingleInstanceManager.WorldObjects.currentExplossion = Instantiate(explossionParticlesPrefab, transform.position, Quaternion.identity, null);
        SingleInstanceManager.Managers.gameManager.PlayerDeath();

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        playerInputAction.action.Disable();
        playerInputAction.action.performed -= PlayerInputDetected;
    }
}
