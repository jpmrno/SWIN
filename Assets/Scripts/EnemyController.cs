using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform ShotSpawn;

    // EnemiesManager sets itself as the manager when instantiating this object.
    public EnemiesManager EnemiesManager { get; set; }
    // TODO: currently unused, but maybe useful for more precise shoting to player
    public int Row { get; set; }
    public int Col { get; set; }

    private void OnDestroy()
    {
        EnemiesManager.DestroyEnemy(gameObject);
    }
}
