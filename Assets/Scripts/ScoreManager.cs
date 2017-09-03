using UnityEngine;
using UnityEngine.UI;

// Taken & Adapted from:
//   https://unity3d.com/learn/tutorials/projects/survival-shooter-tutorial/scoring-points
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string ScoreText;

    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            _playerShipHealth.CheckScoreToGiveLife();
        }
    }

    private const string PlayerShipTag = "Player";
    private PlayerShipHealth _playerShipHealth;
    private Text _text;

    private void Awake()
    {
        Instance = this;
        _score = 0;
        var playerShip = GameObject.FindGameObjectWithTag(PlayerShipTag);
        _playerShipHealth = playerShip.GetComponent<PlayerShipHealth>();
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text =  ScoreText + Score;
    }
}
