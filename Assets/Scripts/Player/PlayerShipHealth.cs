using Gameplay;
using UnityEngine;
using UnityEngine.UI;

// Taken & adapted from:
//   https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
namespace Player
{
    public class PlayerShipHealth : MonoBehaviour // TODO: Have to make it die
    {
        public int ScoreToLiveThreshold;
        public int HealthUnit;
        public int StartingHealth;
        public Slider HealthSlider;
        public Image DamageImage;
        public float FlashSpeed;
        public Color FlashColor;

        private int _currentHealth;
        private bool _isDamaged;
        private int _givenLives;

        private void Awake()
        {
            _currentHealth = StartingHealth;
            HealthSlider.value = _currentHealth;
            _isDamaged = false;
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
            HealthSlider.value = _currentHealth;
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
            HealthSlider.value = _currentHealth;
            _givenLives++;
        }
    }
}
