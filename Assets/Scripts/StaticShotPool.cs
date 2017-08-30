using System.Collections.Generic;
using UnityEngine;

public class StaticShotPool : MonoBehaviour {

    public static StaticShotPool Instance { get; private set; }

    public GameObject shotPrefab;
    public int size = 5;
    public bool grow = true;

    private Queue<GameObject> pool;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pool = new Queue<GameObject>();

        for(int i = 0; i < size; i++) {
            GameObject go = GameObject.Instantiate(shotPrefab, transform) as GameObject;
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject GetShot()
    {
        if (pool.Count == 0) {
            if (!grow) {
                return null;
            }

            GameObject go = GameObject.Instantiate(shotPrefab, transform) as GameObject;
            go.SetActive(false);
            return go;
        }

        return pool.Dequeue();
    }

    public void RecycleShot(GameObject go)
    {
        go.SetActive(false);
        pool.Enqueue(go);
    }
}
