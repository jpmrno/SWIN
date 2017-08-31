using UnityEngine;

public class BoundaryEdge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot")) return; // Ignore trigger if `other` is a shot
        EnemiesManager.Instance.ChangeDirection();
    }
}
