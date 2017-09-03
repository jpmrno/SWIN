using PlainObjects;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float Speed;

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

    protected void OnCollisionEnter2D(Collision2D other)
    {
        var go = other.gameObject;
        if (go.CompareTag("Shot")) go.GetComponent<Shot>().Recycle();
        else Destroy(other.gameObject);
        // Always recycle current shot
        Recycle();
    }
}
