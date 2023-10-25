using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundColorChanger : MonoBehaviour
{
    [SerializeField]
    private float colorDuration = 5f;
    [SerializeField]
    private float transitionDuration = 1f;
    [SerializeField]
    private List<Color> backgroundColors;

    private Camera mainCamera;
    private Color targetColor;
    private Coroutine colorTransitionCoroutine;
    private float transitionTimer = 0f;
    private bool colorChangeAllowed = false;

    private void Awake() => SingletonManager.Player.cameraColorChanger = this;

    private void Start()
    {
        mainCamera = Camera.main;
        colorTransitionCoroutine = StartCoroutine(ColorTransitionRoutine()); // Initialize with a color
    }

    private void Update()
    {
        // Check if color change is allowed and if the system isn't currently in a color transition
        if (colorChangeAllowed && colorTransitionCoroutine == null)
        {
            transitionTimer += Time.deltaTime;

            if (transitionTimer >= colorDuration)
                colorTransitionCoroutine = StartCoroutine(ColorTransitionRoutine());
        }
    }

    // Choose a random color from the list
    private void SetRandomBackgroundColor()
    {
        Color previousColor = targetColor;

        int randomIndex = Random.Range(0, backgroundColors.Count);
        targetColor = backgroundColors[randomIndex];

        if (targetColor == previousColor)
            SetRandomBackgroundColor();
    }

    private IEnumerator ColorTransitionRoutine()
    {
        bool chagingColor = true;

        SetRandomBackgroundColor();

        while (chagingColor)
        {
            float t = 0f;

            Color startColor = mainCamera.backgroundColor;

            // Lerping cycle
            while (t < transitionDuration)
            {
                t += Time.deltaTime / colorDuration;
                mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }

            chagingColor = false;
        }

        // Reset values
        transitionTimer = 0f;
        colorTransitionCoroutine = null;
    }

    /// <summary>
    /// Change the color transition allowing state
    /// </summary>
    /// <param name="state">True to enable, false to disable</param>
    public void ChangeColorChangeAllowedState(bool state) => colorChangeAllowed = state;
}
