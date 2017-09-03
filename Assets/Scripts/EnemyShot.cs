using UnityEngine;

public class EnemyShot : Shot
{
    private new void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) return; // No damage to mates :D
        base.OnCollisionEnter2D(other);
    }
}
