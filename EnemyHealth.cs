using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class EnemyHealth : MonoBehaviour
    {
        Audio_Manager am;
        [SerializeField] private float startingHealth;
        [SerializeField] Animator anim;
        // startingHealth: inputed health in Unity Editor

        public float currentHealth { get; private set; }
        // get means any file can get the data 
        // private set means only this local EnemyHealth can set the health

        private void Awake()
        {
            currentHealth = startingHealth;
            am = GameObject.FindGameObjectWithTag( "Audio" ).GetComponent<Audio_Manager>();
            // instantiates enemy health with starting health stated in 
            // serialized field in unity editor
        }

        public void TakeDamage( float _damage )
        {
            currentHealth = Mathf.Clamp( currentHealth - _damage, 0, startingHealth );
            am.PlaySFX( am.EnemyHit );
            // Mathf.Clamp basically restricts the health to a minimum (0) and a maximum (startHealth)
            // in a sense, the enemy's health now cannot go below 0 or above his maximum health

            if (currentHealth == 0)
            {
                anim.Play("ExplodeEnemy");
                am.PlaySFX( am.EnemyDead );
                StartCoroutine(dissapear(gameObject));
                // enemy dies
            }
        }
        
        IEnumerator dissapear(GameObject gameObject)
        {
            yield return new WaitForSeconds(0.25f);
            Destroy(gameObject);
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