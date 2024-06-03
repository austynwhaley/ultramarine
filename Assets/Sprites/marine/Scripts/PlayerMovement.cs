using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    // character input vars
    public float runSpeed = 55f;
    float horizontalMove = 0f;
    public float thrustSpeed = 60f;
    bool jump = false;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Horizontal") != 0f)
            {
                runSpeed = 85f;
            }
            else { runSpeed = 55f; }

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                ThrustForward();
            }

        }
    }

    void ThrustForward()
    {
        // Calculate thrust direction based on player's facing direction
        float thrustDirection = controller.m_FacingRight ? 1f : -1f;

        // Calculate thrust velocity
        Vector2 thrustVelocity = new Vector2(thrustDirection * thrustSpeed, 0f);

        // Apply the thrust to the player's rigidbody
        controller.GetComponent<Rigidbody2D>().velocity = thrustVelocity;
    }



    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
