using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float Speed;

    public StaticShotPool Pool { get; private set; }

    private void Start()
    {
        Pool = StaticShotPool.Instance;
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
        Pool.RecycleShot(gameObject);
    }
}
