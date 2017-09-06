using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaver : MonoBehaviour
{
    public Storage Storage;
    public Text Letter1;
    public Text Letter2;
    public Text Letter3;

    public void SaveScore()
    {
        Storage.Scores.Add(new KeyValuePair<string, int>(Letter1.text + Letter2.text + Letter3.text,
            ScoreManager.Instance.Score));
        Storage.CurrentHealth = 0;
        Storage.CurrentScore = 0;
    }
}