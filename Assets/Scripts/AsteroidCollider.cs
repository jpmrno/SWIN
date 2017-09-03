using UnityEngine;

public class AsteroidCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var go = other.gameObject;
        if (go.CompareTag("Shot")) go.GetComponent<Shot>().Recycle();
        else Destroy(go);
        Destroy(gameObject);
    }
}
