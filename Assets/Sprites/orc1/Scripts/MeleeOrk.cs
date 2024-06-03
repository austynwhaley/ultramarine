using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeleeOrkSpace
{
    public class MeleeOrk : MonoBehaviour
    {
        [SerializeField] private SimpleFlash flashEffect;
        public int health = 133000;

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
            else
            {
                Flash();
            }
        }

        void Die()
        {
            Destroy(gameObject);
        }

        void Flash()
        {
            // Call the Flash() method of the SimpleFlash component
            flashEffect.Flash();
        }
    }
}
