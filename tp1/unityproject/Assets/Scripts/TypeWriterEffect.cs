using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public Text TextHolder;
    public int LettersPerSecond;
    
    private string _text;
    private float _timeElapsed = 0;

    // Use this for initialization
    private void Start()
    {
        _text = TextHolder.text;
        TextHolder.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        _timeElapsed += Time.deltaTime;

        var text = GetLetters((int) (_timeElapsed * LettersPerSecond));

        TextHolder.text = text;
    }

    private string GetLetters(int letters)
    {
        
        for (var i = 0; i < _text.Length; i++)
        {
            letters--;
            
            if (letters <= 0)
            {
                return _text.Substring(0, i);
            }
        }
        
        return _text;
    }
}