using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresDisplay : MonoBehaviour
{
    public Storage Storage;
    public GameObject Prefab;
    public int Limit;

    // Use this for initialization
    private void Start()
    {
        var i = 1;
        Storage.Scores.Sort((kvp1, kvp2) => kvp2.Value - kvp1.Value);

        foreach (var score in Storage.Scores)
        {
            var textGo = Instantiate(Prefab) as GameObject;
            var text = textGo.GetComponent<Text>();

            text.text = i + ". " + score.Key + " " + score.Value;
            text.transform.SetParent(transform);

            if (i++ == Limit)
            {
                return;
            }
        }
    }
}
