using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform leftPoint;  // The left patrol point
    public Transform rightPoint; // The right patrol point
    public float moveSpeed = 3f; // The speed at which the enemy moves

    private bool movingRight = true; // Determines if the enemy is currently moving right

    void Update()
    {
        // Check if the enemy is moving right
        if (movingRight)
        {
            // Move the enemy towards the right point
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            // If the enemy has reached the right point, switch direction
            if (transform.position.x >= rightPoint.position.x)
            {
                Flip();
            }
        }
        else
        {
            // Move the enemy towards the left point
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            // If the enemy has reached the left point, switch direction
            if (transform.position.x <= leftPoint.position.x)
            {
                Flip();
            }
        }
    }

    // Function to flip the enemy's direction
    void Flip()
    {
        movingRight = !movingRight; // Change the direction
        Vector3 scale = transform.localScale; // Get the current scale
        scale.x *= -1; // Flip the x-axis
        transform.localScale = scale; // Apply the new scale
    }

    // Visualize the patrol points in the Scene view
    void OnDrawGizmosSelected()
    {
        if (leftPoint != null && rightPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(leftPoint.position, 0.1f);
            Gizmos.DrawWireSphere(rightPoint.position, 0.1f);
            Gizmos.DrawLine(leftPoint.position, rightPoint.position);
        }
    }
}
