using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        StaticShotPool.Instance.RecycleShot(other.gameObject);
    }
}
