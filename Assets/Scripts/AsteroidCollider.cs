using UnityEngine;

public class AsteroidCollider : MonoBehaviour
{
    public GameObject AsteroidExplosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyShot") || other.CompareTag("PlayerShot")) other.GetComponent<Shot>().Recycle();
        else Destroy(other);
        Instantiate(AsteroidExplosion, other.transform.position, other.transform.rotation);
        Destroy(gameObject);
    }
}
