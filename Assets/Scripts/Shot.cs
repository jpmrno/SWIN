using PlainObjects;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float Speed;
    public int Damage;

    // ShotPool sets itself as a pool when instantiating this object.
    public ShotPool Pool { private get; set; }

    public void Recycle()
    {
        Pool.RecycleShot(gameObject);
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
    }
}
