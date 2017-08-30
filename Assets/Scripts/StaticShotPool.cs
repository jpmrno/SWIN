using System.Collections.Generic;
using UnityEngine;

public class StaticShotPool : MonoBehaviour 
{
    public static StaticShotPool Instance { get; private set; }

    public GameObject ShotPrefab;
    
    private const int Size = 5;
    private Queue<GameObject> _pool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _pool = new Queue<GameObject>();

        for(var i = 0; i < Size; i++)
        {
            CreateNewShot(_pool);
        }
    }

    public GameObject GetShot()
    {
        // TODO: This CreateNewShot here is temp and we should be prepare to handle a null
        // returning from this function, as the firerate hasn't been decided yet.
        return _pool.Count != 0 ? _pool.Dequeue() : CreateNewShot(_pool);
    }

    public void RecycleShot(GameObject go)
    {
        go.SetActive(false);
        _pool.Enqueue(go);
    }
    
    private GameObject CreateNewShot(Queue<GameObject> pool)
    {
        var go = Instantiate(ShotPrefab, transform);
        go.SetActive(false);
        pool.Enqueue(go);
        return go;
    }
}
