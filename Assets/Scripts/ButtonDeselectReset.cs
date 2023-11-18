using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDeselectReset : MonoBehaviour
{
    private EventSystem eventSystem;

    void Start()
    {
        GameObject sceneEventSystem = GameObject.Find("EventSystem");
        eventSystem = sceneEventSystem.GetComponent<EventSystem>();

        if(TryGetComponent<Button>(out Button button))
            button.onClick.AddListener(ResetEventSystem);

        if(TryGetComponent<Slider>(out Slider slider))
            slider.onValueChanged.AddListener(ResetEventSystemSlider);
    }

    // Reset the EventSystem's selected game object when a button is clicked
    private void ResetEventSystem() => eventSystem.SetSelectedGameObject(null);

    // Reset the EventSystem's selected game object when a slider's value changes
    private void ResetEventSystemSlider(float value) => eventSystem.SetSelectedGameObject(null);
}
