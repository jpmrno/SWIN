using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;
using Random = System.Random;

namespace Enemies
{
    public class EnemiesManager : MonoBehaviour
    {
        public enum Direction { Right, Left }

        private const string CommonName = "Enemy ";
        private const string RowColSplitter = "_ ";

        public static EnemiesManager Instance { get; private set; }

        public Animator Animator;

        public Vector2 InitSpawnPosition;
        public Vector2 EndSpawnPosition;
        public float HorizontalDistBetweenEnemies;
        public float VerticalDistBetweenEnemies;

        public float AbsYBoundary;
        public float AbsXBoundary;
        public float Speed;
        public float GoDownSpeed;
        public float MovingRateTime;
        public Direction CurrentDirection;

        public float FireRate;
        public int ShotPoolSize;

        public GameObject EnemyShot;
        public GameObject Enemy1;
        public GameObject Enemy2;
        public int RowsWithEnemy1;

        private int _rows;
        private int _enemiesPerRow;
        private List<GameObject> Enemies { get; set; }
        private float _remainingTimeToMove;

        private const float MovingRateTimeEnemyWeightFactor = 3f;
        private const float FireRateEnemyFactor = 1f;

        private float _remainingCoolDownTime;
        private ShotPool ShotPool { get; set; }
        private const bool ShotPoolGrow = false;
        private float _enemyWeight;

        private readonly Random _random = new Random();

        private void Awake()
        {
            if (Instance == null) Instance = this; // else, this instance will not be used at all
            ShotPool = new ShotPool(ShotPoolSize, ShotPoolGrow, EnemyShot);
            Enemies = new List<GameObject>();
            _rows = CalculateRows();
            _enemiesPerRow = CalculateEnemiesPerRow();
            var initialEnemiesCount = _rows * _enemiesPerRow;
            _enemyWeight = 1f / initialEnemiesCount;
        }

        private int CalculateEnemiesPerRow()
        {
            return (int) Math.Ceiling(Math.Abs(EndSpawnPosition.x - InitSpawnPosition.x) / HorizontalDistBetweenEnemies);
        }

        private int CalculateRows()
        {
            return (int) Math.Round(Math.Abs(EndSpawnPosition.y - InitSpawnPosition.y) / VerticalDistBetweenEnemies);
        }

        // Use this for initialization
        private void Start () {
            for (var i = 0; i < _rows; i++)
            {
                var y = InitSpawnPosition.y - VerticalDistBetweenEnemies * i;
                var enemyModel = i < RowsWithEnemy1 ? Enemy1 : Enemy2;
                for (var j = 0; j < _enemiesPerRow; j++)
                {
                    var x = InitSpawnPosition.x + HorizontalDistBetweenEnemies * j;
                    var position = new Vector2(x, y);
                    // Create this kind of enemy, without rotation.
                    var enemy = Instantiate(enemyModel, position, Quaternion.identity);
                    enemy.name = BuildEnemyName(i, j);
                    enemy.transform.SetParent(transform); // Set parent to this.
                    // Set itself as the manager.
                    var enemyController = enemy.GetComponent<EnemyController>();
                    enemyController.EnemiesManager = this;
                    // Add the enemy to the enemies trops.
                    Enemies.Add(enemy);
                }
            }

        }

        private static string BuildEnemyName(int i, int j)
        {
            return CommonName + (i + 1) + RowColSplitter + (j + 1);
        }

        // Update is called once per frame
        public void Update()
        {
            MoveIfItsTimeTo();
            ShootIfItsTimeTo();
        }

        private void ShootIfItsTimeTo()
        {
            // Only shoot if the weapon has finished cooling down.
            _remainingCoolDownTime -= Time.deltaTime;
            if (_remainingCoolDownTime > 0) return;
            // Time to shoot
            Shoot();
            // Cold down the weapon
            _remainingCoolDownTime = FireRate;
        }

        private void Shoot()
        {
            // Get the bolt to shoot.
            GameObject boltGameObject = ShotPool.Dequeue();
            if (boltGameObject == null) return; // There was no bolt => cannot shoot.

            // Get a remaining enemy ship and make it shot
            var enemy = Enemies.ElementAt(_random.Next(Enemies.Count));
            var enemyController = enemy.GetComponent<EnemyController>();

            // Position the bolt to be correctly fired & enable it
            boltGameObject.transform.position = enemyController.ShotSpawn.position;
            boltGameObject.transform.rotation = enemyController.ShotSpawn.rotation;
            boltGameObject.SetActive(true);
        }

        private void MoveIfItsTimeTo()
        {
            // Only move when it's time to.
            _remainingTimeToMove -= Time.deltaTime;
            if (!(_remainingTimeToMove < 0)) return;
            // Time to move.
            Move();
            CheckIfInvasionIsCompleted();
            _remainingTimeToMove = MovingRateTime;
        }

        // Handle game over case when enemies have reached some close to player
        private void CheckIfInvasionIsCompleted()
        {
            if (!Enemies.Any(enemy => Math.Abs(enemy.transform.position.y) <= AbsYBoundary)) return;
            Animator.SetTrigger("Game Over");
        }

        private void Move()
        {
            if (IsTimeToChangeDirection())
            {
                CurrentDirection = CurrentDirection.Equals(Direction.Right) ? Direction.Left : Direction.Right;
                transform.position = GoDown();
            }
            // Include both moves on purpose: Down + (Right || Left).
            transform.position = CurrentDirection.Equals(Direction.Right) ? GoRight() : GoLeft();
        }

        private bool IsTimeToChangeDirection()
        {
            return Enemies.Select(enemy => Math.Abs(enemy.transform.position.x) + Math.Abs(Speed)).Any(newX => newX >= AbsXBoundary);
        }

        private Vector2 GoDown()
        {
            return Move(transform.position.x, transform.position.y - GoDownSpeed);
        }

        private Vector2 GoRight()
        {
            return Move(transform.position.x + Speed, transform.position.y);
        }

        private Vector2 GoLeft()
        {
            return Move(transform.position.x - Speed, transform.position.y);
        }

        private static Vector2 Move(float newX, float newY)
        {
            return new Vector2(newX, newY);
        }

        public void DestroyEnemy(GameObject enemy)
        {
            Enemies.Remove(enemy);
            // With less enemies, increase moving moments and increase time between shots
            MovingRateTime -= _enemyWeight * MovingRateTime * MovingRateTimeEnemyWeightFactor;
            FireRate += _enemyWeight * FireRate * FireRateEnemyFactor;
            if (Enemies.Any()) return;
            Animator.SetTrigger("Victory");
            Destroy(gameObject);
        }
    }
}
