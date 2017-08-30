using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour 
{
    private void OnTriggerExit(Collider other)
    {
        StaticShotPool.Instance.RecycleShot(other.gameObject);
    }
}
