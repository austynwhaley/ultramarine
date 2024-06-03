using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MeleeOrkSpace
{
    public class Weapon : MonoBehaviour
    {
        // this script uses raycasting for bullet handling
        public Transform firePoint;
        public int damageAmount;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            RaycastHit2D hitResponse = Physics2D.Raycast(firePoint.position, firePoint.right);

            if (hitResponse)
            {
                MeleeOrk meleeOrk = hitResponse.transform.GetComponent<MeleeOrk>();
                if (meleeOrk)
                {
                    meleeOrk.TakeDamage(damageAmount);
                }

            }

            // Debug visualization of the raycast
            Debug.DrawRay(firePoint.position, firePoint.right * hitResponse.distance, Color.red, 0.1f);
        }

    }
}
