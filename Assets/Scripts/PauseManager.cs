using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public Animator Animator;
    
    private bool _paused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!_paused)
        {
            Animator.SetTrigger("Pause");
            Time.timeScale = 0;
            _paused = true;
        }
        else
        {
            Animator.SetTrigger("Play");
            Time.timeScale = 1;
            _paused = false;
        }
    }
}