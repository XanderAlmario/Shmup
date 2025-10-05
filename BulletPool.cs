using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        private Queue<GameObject> pool = new Queue<GameObject>();

        public GameObject GetObject()
        {
            if (pool.Count > 0)
            {
                GameObject bullet = pool.Dequeue();
                bullet.SetActive(true);
                return bullet;
            }

            return Instantiate(prefab);
        }

        public void ReturnObject(GameObject bullet)
        {
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }
}
