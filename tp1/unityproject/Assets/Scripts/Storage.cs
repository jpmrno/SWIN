using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Storage : ScriptableObject
{
    public int CurrentHealth = 0;
    public int CurrentScore = 0;
    public List<KeyValuePair<string, int>> Scores = new List<KeyValuePair<string, int>>();
}