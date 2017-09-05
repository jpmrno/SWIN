using Gameplay;
using UnityEngine;
using UnityEngine.UI;

// Taken & adapted from:
//   https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
namespace Player
{
    public class PlayerShipHealth : MonoBehaviour
    {
        private static readonly Color Red = new Color(0.39f, 0f, 0f, 1);
        private static readonly Color Green = new Color(0f, 0.39f, 0f, 1);

        public int ScoreToLiveThreshold;
        public int HealthUnit;
        public int MaxHealth;
        public int StartingHealth;
        public Slider HealthSlider;
        public Image Fill;
        public Image DamageImage;
        public float FlashSpeed;
        public Color FlashColor;

        private const string PlayerShipTag = "Player";
        private PlayerShipController _playerShipController;
        private int _currentHealth;
        private bool _isDamaged;
        private int _givenLives;

        private void Awake()
        {
            _currentHealth = StartingHealth;
            HealthSlider.maxValue = MaxHealth;
            HealthSlider.minValue = 0;
            HealthSlider.value = _currentHealth;
            _isDamaged = false;
        }

        private void Start()
        {
            var playerShip = GameObject.FindGameObjectWithTag(PlayerShipTag);
            _playerShipController = playerShip.GetComponent<PlayerShipController>();
        }

        private void Update()
        {
            DamageImage.color = _isDamaged ? FlashColor : Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
            _isDamaged = false;
        }

        public void TakeShot(int damageAmount)
        {
            _isDamaged = true;
            _currentHealth -= damageAmount;
            UpdateHealthSlider();
            if (_currentHealth <= 0) _playerShipController.Destroyed();
        }

        public void CheckScoreToGiveLife()
        {
            if (ScoreManager.Instance.Score / ScoreToLiveThreshold <= _givenLives) return;
            IncrementHealth();
        }

        private void IncrementHealth()
        {
            if (_currentHealth >= StartingHealth) return;
            _currentHealth += HealthUnit;
            UpdateHealthSlider();
            _givenLives++;
        }

        private void UpdateHealthSlider()
        {
            HealthSlider.value = _currentHealth;
            // TODO: Recall that StartingHealth is consider to be always > 0
            Fill.color = Color.Lerp(Red, Green, (float) _currentHealth / StartingHealth);
        }
    }
}
