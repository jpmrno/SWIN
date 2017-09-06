using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Player;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public Storage Storage;
    public PlayerShipHealth HealthManager;

    public void SaveSession()
    {
        Storage.CurrentHealth = HealthManager.CurrentHealth;
        Storage.CurrentScore = ScoreManager.Instance.Score;
    }
}