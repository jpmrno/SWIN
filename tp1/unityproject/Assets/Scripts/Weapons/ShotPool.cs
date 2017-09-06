using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class ShotPool
    {
        private int Size { get; set; }
        private bool Grow { get; set; }
        private GameObject Shot { get; set; }
        private Queue<GameObject> Pool { get; set; }

        public ShotPool(int size, bool grow, GameObject shot)
        {
            Size = size;
            Grow = grow;
            Shot = shot;
            Pool = new Queue<GameObject>();

            for(var i = 0; i < Size; i++)
            {
                CreateNewShot();
            }
        }

        public GameObject Dequeue()
        {
            return Pool.Any() ? Pool.Dequeue() : Grow ? CreateNewShot().Dequeue() : null;
        }

        public void RecycleShot(GameObject go)
        {
            go.SetActive(false);
            if (Pool.Contains(go)) return; // go has already collided with other object.
            Pool.Enqueue(go);
        }

        private Queue<GameObject> CreateNewShot()
        {
            var go = Object.Instantiate(Shot);
            go.GetComponent<Shot>().Pool = this;
            go.SetActive(false);
            Pool.Enqueue(go);
            return Pool;
        }
    }
}
