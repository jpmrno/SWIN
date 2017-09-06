using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    private Text _display;

    private void Start()
    {
        _display = GetComponent<Text>();
        _display.text = ScoreManager.Instance.Score.ToString();
    }

    private void Update()
    {
        _display.text = ScoreManager.Instance.Score.ToString();
    }
}