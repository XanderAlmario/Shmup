using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float bulletSpeed;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] BulletPool bPool;
        [SerializeField] PlayerHealth HP;
        Animator animator;

        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            if (HP.currentHealth != 0)
            {
                float hInput = Input.GetAxis("Horizontal");
                float vInput = Input.GetAxis("Vertical");

                rb.linearVelocity = new Vector2(hInput * speed, vInput * speed);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Shoot();
                }
            }
            else
            {
                rb.linearVelocity = new Vector2(0, 0);
            }
        }

        private void Shoot()
        {
            GameObject bullet = bPool.GetObject();
            var bulletStartPos = new Vector2(transform.position.x, transform.position.y + 1.2f);
            bullet.transform.position = bulletStartPos;

            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

            if (rbBullet != null) rbBullet.linearVelocity = new Vector2(0, bulletSpeed);
            animator.SetBool("shooting", true);

            StartCoroutine(DeactivateBullet(bullet));
        }

        IEnumerator DeactivateBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(0.2f);
            animator.SetBool("shooting", false);
            yield return new WaitForSeconds(0.8f);
            bPool.ReturnObject(bullet);
        }
    }
}