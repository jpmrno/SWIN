using System;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public enum Direction { Right, Left }

    private const string CommonName = "Enemy ";
    private const string RowColSplitter = "_ ";

    public static EnemiesManager Instance { get; private set; }

    public Vector2 InitSpawnPosition;
    public Vector2 EndSpawnPosition;
    public float HorizontalDistBetweenMonsters;
    public float VerticalDistBetweenMonsters;


    public float Speed;
    public float GoDownSpeed;
    public float MovingRateTime;
    public Direction CurrentDirection;

    public GameObject Enemy1;
    public GameObject Enemy2;
    public int RowsWithEnemy1;

    private int _rows;
    private int _monstersPerRow;
    private GameObject[][] _monsters;
    private float _remainingTimeToMove;
    private Direction NextDirection { get; set; }

    private void Awake()
    {
        Instance = this;
        _rows = CalculateRows();
        _monstersPerRow = CalculateMonstersPerRow();
    }

    private int CalculateMonstersPerRow()
    {
       return (int) Math.Ceiling(Math.Abs(EndSpawnPosition.x - InitSpawnPosition.x) / HorizontalDistBetweenMonsters);
    }

    private int CalculateRows()
    {
        return (int) Math.Round(Math.Abs(EndSpawnPosition.y - InitSpawnPosition.y) / VerticalDistBetweenMonsters);
    }

    // Use this for initialization
    private void Start () {
        _monsters = new GameObject[_rows][];
        for (var i = 0; i < _rows; i++)
        {
            var row = new GameObject[_monstersPerRow];
            var y = InitSpawnPosition.y - VerticalDistBetweenMonsters * i;
            var enemyModel = i < RowsWithEnemy1 ? Enemy1 : Enemy2;
            for (var j = 0; j < _monstersPerRow; j++)
            {
                var x = InitSpawnPosition.x + HorizontalDistBetweenMonsters * j;
                var position = new Vector2(x, y);
                // Create this kind of moster, without rotation
                var monster = Instantiate(enemyModel, position, Quaternion.identity);
                monster.name = BuildEnemyName(i, j);
                monster.transform.SetParent(transform); // Set parent to this.
                // Assign row and column inside its controller for a better search when deleted.
                var monsterController = monster.GetComponent<EnemyController>();
                monsterController.Row = i;
                monsterController.Col = j;
                row[j] = monster;
            }
            _monsters[i] = row;
        }

    }

    private static string BuildEnemyName(int i, int j)
    {
        return CommonName + (i + 1) + RowColSplitter + (j + 1);
    }

    // Update is called once per frame
    public void Update()
    {
        // Only move when it's time to.
        _remainingTimeToMove -= Time.deltaTime;
        if (!(_remainingTimeToMove < 0)) return;
        // Time to move.
        Move();
        _remainingTimeToMove = MovingRateTime;
    }

    private void Move()
    {
        if (NextDirection != CurrentDirection)
        {
            CurrentDirection = NextDirection; // Reset for next `GoDown` case
            transform.position = GoDown();
        }
        // Include both moves on purpose: Down + (Right || Left).
        transform.position = CurrentDirection.Equals(Direction.Right) ? GoRight() : GoLeft();
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

    public void ChangeDirection()
    {
        NextDirection = CurrentDirection == Direction.Right ? Direction.Left : Direction.Right;
    }
}
