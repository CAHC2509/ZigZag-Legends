using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadePanel : MonoBehaviour
{
    [Header("Fade Events")]
    [Space, SerializeField]
    private UnityEvent onFadeCompleted;
    [Space, SerializeField]
    private UnityEvent onUnfadeCompleted;

    public void FadeCompleted() => onFadeCompleted.Invoke();

    public void UnfadeCompleted() => onUnfadeCompleted.Invoke();
}
