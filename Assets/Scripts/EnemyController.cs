using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform ShotSpawn;
    public int Score;

    // EnemiesManager sets itself as the manager when instantiating this object.
    public EnemiesManager EnemiesManager { get; set; }
    public GameObject EnemyExplosion;

    private void OnDestroy()
    {
        EnemiesManager.DestroyEnemy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyShot")) return;
        if (other.CompareTag("PlayerShot"))
        {
            Instantiate(EnemyExplosion, transform.position, transform.rotation);
            ScoreManager.Instance.Score += Score;
        }
        Destroy(gameObject);
    }
}
