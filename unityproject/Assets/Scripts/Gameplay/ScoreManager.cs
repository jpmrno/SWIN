using Player;
using UnityEngine;
using UnityEngine.UI;

// Taken & Adapted from:
//   https://unity3d.com/learn/tutorials/projects/survival-shooter-tutorial/scoring-points
namespace Gameplay
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public Storage Storage;
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
            if (Instance == null) Instance = this; // else, this instance will not be used at all
            _score = Storage.CurrentScore;
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            var playerShip = GameObject.FindGameObjectWithTag(PlayerShipTag);
            _playerShipHealth = playerShip.GetComponent<PlayerShipHealth>();
        }

        private void Update()
        {
            _text.text =  ScoreText + Score;
        }
    }
}
