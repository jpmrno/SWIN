using UnityEngine;

public class MonsterController : MonoBehaviour 
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        StaticShotPool.Instance.RecycleShot(other.gameObject);
    }

}
