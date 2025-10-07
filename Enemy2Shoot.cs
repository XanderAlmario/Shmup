using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Enemy2Shoot : MonoBehaviour
    {
        [SerializeField]  float bulletSpeed = -5f;
        [SerializeField]  BulletPool eBPool;
        [SerializeField]  int counter = 100;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (counter > 0)
            {
                counter -= 1;
            }
            else if (counter <= 0)
            {
                Shoot();
                counter = 50;
            }
        }

        
        private void Shoot()
        {
            GameObject bullet = eBPool.GetObject();
            var bulletStartPos = new Vector2(transform.position.x, transform.position.y - 0.5f);
            bullet.transform.position = bulletStartPos;

            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

            if (rbBullet != null) rbBullet.linearVelocity = new Vector2(0, bulletSpeed);

            StartCoroutine(DeactivateBullet(bullet));
        }

        IEnumerator DeactivateBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(0.8f);
            eBPool.ReturnObject(bullet);
        }
    }
}
