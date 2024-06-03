using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    Rigidbody2D rb;
    public bool leftRight;
    public bool jump;
    public float maxDistance;
    public LayerMask layerMask;
    public Vector3 boxSize;
    bool isHurting, isDead;
    private Animator anim;
    private PlayerHealth playerHealth;
    private bool isJumping = false; // Track if the player is already jumping
    private int meleeComboCount = 0;
    private float lastMeleeTime = 0f;
    public float meleeComboWindow = 0.5f; // Time window to chain melee attacks


    void Start()
    {
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        leftRight = Input.GetAxisRaw("Horizontal") != 0f;
        jump = Input.GetButtonDown("Jump");

        // Handle melee attack
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(meleeComboCount);
            if (Time.time - lastMeleeTime > meleeComboWindow)
            {
                // Reset combo if too much time has passed since last attack
                meleeComboCount = 0;
            }

            lastMeleeTime = Time.time;

            meleeComboCount++;
            if (meleeComboCount > 3) meleeComboCount = 1; // Loop back to first attack if combo exceeds 3

            switch (meleeComboCount)
            {
                case 1:
                    anim.SetTrigger("Melee1");
                    Debug.Log("melee1");
                    Debug.Log(meleeComboCount);
                    break;
                case 2:
                    anim.SetTrigger("Melee2");
                    Debug.Log("melee2");
                    Debug.Log(meleeComboCount);
                    break;
                case 3:
                    anim.SetTrigger("Melee3");
                    Debug.Log("melee3");
                    Debug.Log(meleeComboCount);
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("Slide");
        }

        if (!leftRight)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
        if (Input.GetButtonUp("Jump"))
        {
            // Reset isJumping when the jump button is released
            isJumping = false;
            anim.SetBool("isJumping", false);
        }
        if (leftRight && !anim.GetBool("isRunning"))
        {
            anim.SetBool("isWalking", true);
        }
        if (leftRight && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (jump && GroundCheck() && !Physics.Raycast(transform.position, -transform.up, 0.05f))
        {
            // Trigger jump animation only if not already jumping
            if (!isJumping)
            {
                anim.SetTrigger("Jump");
                isJumping = true;
            }
        }
        if (playerHealth.health <= 0)
        {
            anim.SetTrigger("isDead");
        }
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("isShooting", true);
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    private bool GroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && anim.GetBool("isWalking"))
        {
            anim.SetBool("isWalking", false);
        }
    }
}
