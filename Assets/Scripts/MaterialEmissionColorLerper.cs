using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmissionColorLerper : MonoBehaviour
{
    [SerializeField]
    private Material targetMaterial;
    [SerializeField]
    private List<Color> emissionColors;
    [SerializeField]
    private float lerpDuration = 1.0f;

    private Color currentColor;
    private Color targetColor;
    private float lerpTimer;
    private int currentIndex;
    private int previousIndex = -1;

    private void Awake()
    {
        currentColor = targetMaterial.GetColor("_EmissionColor");
        StartRandomColorLerp(); // Start the first transition
    }

    private void Update()
    {
        if (lerpTimer < lerpDuration)
        {
            // Perform the Lerp of the emission color
            lerpTimer += Time.deltaTime;
            float t = lerpTimer / lerpDuration;
            Color lerpedColor = Color.Lerp(currentColor, targetColor, t);
            targetMaterial.SetColor("_EmissionColor", lerpedColor);
        }
        else
        {
            // When a transition is complete, start a new one
            StartRandomColorLerp();
        }
    }

    private void StartRandomColorLerp()
    {
        int randomIndex = Random.Range(0, emissionColors.Count);

        // Ensure the new random color is not the same as the previous one
        while (randomIndex == previousIndex)
            randomIndex = Random.Range(0, emissionColors.Count);

        // Update the previous index to the current one
        previousIndex = randomIndex;

        currentColor = targetMaterial.GetColor("_EmissionColor");
        targetColor = emissionColors[randomIndex];
        lerpTimer = 0f;
    }
}
