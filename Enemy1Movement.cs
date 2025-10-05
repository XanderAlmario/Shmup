using UnityEngine;

namespace Shmup
{
    public class Enemy1Movement : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        private void FixedUpdate()
        {
            Vector2 pos = transform.position;

            pos.y -= speed * Time.fixedDeltaTime;

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
