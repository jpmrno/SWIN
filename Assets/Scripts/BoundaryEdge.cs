using UnityEngine;

public class BoundaryEdge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only if `other` is an Enemy
        if (other.CompareTag("Enemy")) EnemiesManager.Instance.ChangeDirection();
    }
}
