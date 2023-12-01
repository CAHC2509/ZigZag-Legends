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
    private int currentIndex = 0;

    private void Awake()
    {
        currentColor = targetMaterial.GetColor("_EmissionColor");
        StartNextColorLerp(); // Start the first transition
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
            StartNextColorLerp();
        }
    }

    private void StartNextColorLerp()
    {
        // Avanza al siguiente color
        currentIndex = (currentIndex + 1) % emissionColors.Count;

        currentColor = targetMaterial.GetColor("_EmissionColor");
        targetColor = emissionColors[currentIndex];
        lerpTimer = 0f;
    }
}
