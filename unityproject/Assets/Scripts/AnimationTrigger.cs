using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// TODO: Remove
public class AnimationTrigger : MonoBehaviour
{
    public Animator Animator;

    public void Trigger(string triggerName)
    {
        EventSystem.current.SetSelectedGameObject(null);
        Animator.SetTrigger(triggerName);
    }
}