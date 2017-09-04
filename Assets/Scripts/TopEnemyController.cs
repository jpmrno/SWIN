using System;
using UnityEngine;
using Random = System.Random;

public class TopEnemyController : MonoBehaviour
{
    public GameObject Explosion;

    public int Score;

    public float Speed;
    public double MinTimeToAppear;
    public double MaxTimeToAppear;

    private Vector2 _originalPosition;
    private double _timeToNextAppearance;
    private Random _random;
    private bool _active;

    private void Start()
    {
        // This in favor of `SetActive` so as the `Update` method is called
        _active = false;
        _originalPosition = transform.position;
        _random = new Random();
        NewTimeToNextAppearance();
    }

    private void NewTimeToNextAppearance()
    {
        _timeToNextAppearance = (MaxTimeToAppear - MinTimeToAppear) * _random.NextDouble() + MinTimeToAppear;
    }

    private void Update()
    {
        if (_active) Move();
        else AppearIfItsTimeTo();
    }

    private void Move()
    {
        var newX = transform.position.x + Speed;
        var newY = transform.position.y;
        transform.position = new Vector2(newX, newY);
        if (Math.Abs(newX) >= Math.Abs(_originalPosition.x)) Disappear();
    }

    private void AppearIfItsTimeTo()
    {
        // Appear if its time to
        _timeToNextAppearance -= Time.deltaTime;
        if (_timeToNextAppearance > 0) return;
        // Time to shoot
        Appear();
    }

    private void Appear()
    {
        _active = true;
        MusicController.Instance.EnterEnemy();
    }

    public void Disappear()
    {
        _active = false;
        MusicController.Instance.ExitEnemy();
        transform.position = _originalPosition;
        // Set time for next appearance
        NewTimeToNextAppearance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // We consider (and should be always true) that only player shots call this function
        ScoreManager.Instance.Score += Score;
        Instantiate(Explosion, transform.position, transform.rotation);
        other.GetComponent<Shot>().Recycle();
        Disappear();
    }
}
