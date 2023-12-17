using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsAfterTime : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 1.5f;
    [Space, SerializeField]
    private UnityEvent onTimeCompeted;

    private IEnumerator EventCoroutine()
    {
        yield return new WaitForSeconds(waitTime);

        onTimeCompeted?.Invoke();   
    }

    /// <summary>
    /// Execute some defined unity events after a delay
    /// </summary>
    public void ExecuteEventWithDelay() => StartCoroutine(EventCoroutine());
}
