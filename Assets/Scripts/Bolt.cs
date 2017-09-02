using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float Speed;

    private void Start ()
    {
        var rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * Speed;
    }
}
