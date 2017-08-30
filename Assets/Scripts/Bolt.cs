using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour {

    public float speed = 1f;

	void Start ()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
	}
}
