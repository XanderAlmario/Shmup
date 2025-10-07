using UnityEngine;

namespace Shmup
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float startingHealth;
        // startingHealth: inputed health in Unity Editor

        public float currentHealth { get; private set; }
        // get means any file can get the data 
        // private set means only this local EnemyHealth can set the health

        private void Awake()
        {
            currentHealth = startingHealth;
            // instantiates enemy health with starting health stated in 
            // serialized field in unity editor
        }

        public void TakeDamage( float _damage )
        {
            currentHealth = Mathf.Clamp( currentHealth - _damage, 0, startingHealth );
            // Mathf.Clamp basically restricts the health to a minimum (0) and a maximum (startHealth)
            // in a sense, the enemy's health now cannot go below 0 or above his maximum health

            if( currentHealth == 0 )
            {
                Destroy(gameObject);
                // enemy dies
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Bullet")) 
            // each individual Bullet has a object tag of Bullet
            {
                TakeDamage(1);
                //Debug.Log(currentHealth);
            }
            else if (collider.CompareTag("Player"))
            {
                TakeDamage(2);
                //Debug.Log(currentHealth);
            }
        }
    }
}
