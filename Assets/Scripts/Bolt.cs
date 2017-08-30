using UnityEngine;

public class Bolt : MonoBehaviour 
{
    private const float Speed = 5f;

    private void Start ()
    {
        var rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * Speed;
    }
}
