using UnityEngine;

namespace Shmup
{
    public class Enemy2Movement : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            gameObject.transform.parent = Camera.main.transform;
        }
        private void FixedUpdate()
        {
            Vector2 pos = transform.position;

            pos.x += speed * Time.fixedDeltaTime;

            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Despawner"))
            {
                Destroy(gameObject);
            }
        }
    }
}
