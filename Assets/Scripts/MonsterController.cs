using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        StaticShotPool.Instance.RecycleShot(other.gameObject);
    }

}
