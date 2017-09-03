using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D shot)
    {
        shot.GetComponent<Shot>().Recycle();
    }
}
