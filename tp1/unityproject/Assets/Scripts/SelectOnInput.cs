using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    public EventSystem EventSystem;
    public GameObject SelectedGameObject;

    private bool _selected;

    private void Update()
    {
        if (!(Math.Abs(Input.GetAxisRaw("Vertical")) > 0) || _selected != false) return;

        EventSystem.SetSelectedGameObject(SelectedGameObject);
        _selected = true;
    }

    private void OnDisable()
    {
        _selected = false;
    }
}