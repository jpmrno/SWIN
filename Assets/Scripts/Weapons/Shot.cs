using System;
using UnityEngine;

namespace Weapons
{
    public class Shot : MonoBehaviour
    {
        
        public GameObject ShotExplosion;

        public float Speed;
        public int Damage;

        // ShotPool sets itself as a pool when instantiating this object.
        public ShotPool Pool { private get; set; }

        private void Recycle()
        {
            Pool.RecycleShot(gameObject);
        }

        private void OnEnable()
        {
            GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.CompareTag("EnemyShot") && other.CompareTag("Enemy")) return;
            Recycle();
        }

        // Should be called only when colliding with another laser
        private void OnCollisionEnter2D(Collision2D other)
        {
            Recycle();
            if (other.gameObject.CompareTag("EnemyShot") && this.CompareTag("PlayerShot"))
            {
                    Instantiate(ShotExplosion, transform.position, transform.rotation);   
            }
            Shot shot = other.gameObject.GetComponent<Shot>();
            // If they are both the same kind of shots => do not recycle the other shot (just one)
            if (shot == null || other.gameObject.CompareTag(gameObject.tag)) return;
            shot.Recycle();
        }
    }
}
