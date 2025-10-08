using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shmup
{
    public class PlayerHealth : MonoBehaviour
    {
        Audio_Manager am;
        [SerializeField] private float startingHealth;
        [SerializeField] Animator anim;
        // startingHealth: inputed health in Unity Editor

        public float currentHealth { get; private set; }
        // get means any file can get the data 
        // private set means only this local Player_Health can set the health

        private int invulnerable = 0;

        private void Awake()
        {
            currentHealth = startingHealth;
            am = GameObject.FindGameObjectWithTag( "Audio" ).GetComponent<Audio_Manager>();
            // instantiates player health with starting health stated in 
            // serialized field in unity editor
        }

        private void Update()
        {
            if (invulnerable > 0)
            {
                invulnerable -= 1;
            }
        }

        public void TakeDamage(float _damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            am.PlaySFX( am.PlayerHit );
            // Mathf.Clamp basically restricts the health to a minimum (0) and a maximum (startHealth)
            // in a sense, the player's health now cannot go below 0 or above his maximum health

            if (currentHealth == 0)
            {
                anim.Play("Death");
                am.PlaySFX( am.GameOverSfx );
                StartCoroutine(gameOver());
                // since the level restarts, the player's health goes back to the original starting health (6)
            }
        }

        IEnumerator gameOver()
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadSceneAsync( "DeathScreen" );
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (invulnerable <= 0)
            {
                if (collider.CompareTag("Enemy"))
                // each individual Bullet has a object tag of Bullet
                {
                    TakeDamage(1);
                    invulnerable = 2;
                    Debug.Log(currentHealth);
                }
                else if (collider.CompareTag("EnemyBullet"))
                {
                    TakeDamage(1);
                    invulnerable = 2;
                    Debug.Log(currentHealth);
                }
            }
        }
    }
}