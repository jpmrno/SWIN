using UnityEngine;
using UnityEngine.UI;

// Taken & adapted from:
//   https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
public class PlayerShipHealth : MonoBehaviour // TODO: Have to make it die
{
    public int ScoreToLiveThreshold;
    public int HealthUnit;
    public int StartingHealth;
    public int CurrentHealth { get; private set; } // check if can be private
    public Slider HealthSlider;
    public Image DamageImage;
    public float FlashSpeed;
    public Color FlashColor;

    private bool _isDamaged;
    private int _givenLives;

    private void Awake()
    {
        CurrentHealth = StartingHealth;
        HealthSlider.value = CurrentHealth;
        _isDamaged = false;
    }

    private void Update()
    {
        // TODO: Test if I can do it directly on the take shot as
        // DamageImage.color = Color.Lerp(FlashColor, Color.clear, FlashSpeed * Time.deltaTime);
        // This implies not needing the _isDamaged variable
        DamageImage.color = _isDamaged ? FlashColor : Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        _isDamaged = false;
    }

    public void TakeShot(int damageAmount)
    {
        _isDamaged = true;
        CurrentHealth -= damageAmount;
        HealthSlider.value = CurrentHealth;
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
        HealthSlider.value = CurrentHealth;
        _givenLives++;
    }
}
