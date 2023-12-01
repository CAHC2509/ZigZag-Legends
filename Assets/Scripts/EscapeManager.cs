using UnityEngine;
using UnityEngine.Events;

public class EscapeManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onEscapePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            onEscapePressed?.Invoke();
    }
}
