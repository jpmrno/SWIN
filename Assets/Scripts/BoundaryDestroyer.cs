using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D bolt)
    {
        bolt.GetComponent<PlayerShot>().Pool.RecycleShot(bolt.gameObject);
    }
}
