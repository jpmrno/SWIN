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

        public Storage Storage;

        public int ScoreToLiveThreshold;
        public int HealthUnit;
        public int MaxHealth;
        public int StartingHealth;
        public Slider HealthSlider;
        public Image Fill;
        public Image DamageImage;
        public float FlashSpeed;
        public Color FlashColor;
        public int CurrentHealth { get; private set; }

        private const string PlayerShipTag = "Player";
        private PlayerShipController _playerShipController;
        private bool _isDamaged;
        private int _givenLives;

        private void Awake()
        {
            CurrentHealth = Storage.CurrentHealth > 0 ? Storage.CurrentHealth : StartingHealth;
            HealthSlider.maxValue = MaxHealth;
            HealthSlider.minValue = 0;
            HealthSlider.value = CurrentHealth;
            _isDamaged = false;
        }

        private void Start()
        {
            var playerShip = GameObject.FindGameObjectWithTag(PlayerShipTag);
            _playerShipController = playerShip.GetComponent<PlayerShipController>();
            _givenLives = ScoreManager.Instance.Score / ScoreToLiveThreshold;
        }

        private void Update()
        {
            DamageImage.color = _isDamaged
                ? FlashColor
                : Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
            _isDamaged = false;
        }

        public void TakeShot(int damageAmount)
        {
            _isDamaged = true;
            CurrentHealth -= damageAmount;
            UpdateHealthSlider();
            if (CurrentHealth <= 0) _playerShipController.Destroyed();
        }

        public void CheckScoreToGiveLife()
        {
            if (ScoreManager.Instance.Score / ScoreToLiveThreshold <= _givenLives) return;
            IncrementHealth();
        }

        private void IncrementHealth()
        {
            if (CurrentHealth >= StartingHealth) return;
            CurrentHealth += HealthUnit;
            UpdateHealthSlider();
            _givenLives++;
        }

        private void UpdateHealthSlider()
        {
            HealthSlider.value = CurrentHealth;
            // TODO: Recall that StartingHealth is consider to be always > 0
            Fill.color = Color.Lerp(Red, Green, (float) CurrentHealth / StartingHealth);
        }
    }
}
