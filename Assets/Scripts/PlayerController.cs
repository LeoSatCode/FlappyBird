using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _thisRigidbody; 
    public float jumpForce = 10f; 
    public float jumpInterval = 0.5f; 
    private float _jumpCooldown = 0f; 

    void Start()
    {
        // Cache the Rigidbody component at the start
        _thisRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Update cooldown timer
        _jumpCooldown -= Time.deltaTime;

        // Check if the game is active and jumps are allowed
        bool isGameActive = GameManager.Instance.IsGameActive();
        bool canJump = _jumpCooldown <= 0f && isGameActive;

        // Handle the jump input if jumping is allowed
        if (canJump)
        {
            bool jumpInput = Input.GetKeyDown(KeyCode.Space);
            if (jumpInput)
            {
                Jump();
            }
        }

        // Toggle gravity based on whether the game is active
        _thisRigidbody.useGravity = isGameActive;
    }

    void OnTriggerEnter(Collider other)
    {
        // Handle trigger-based collisions
        OnCustomCollisionEnter(other.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        // Handle regular collisions
        OnCustomCollisionEnter(other.gameObject);
    }

    private void OnCustomCollisionEnter(GameObject other)
    {
        // Check if the collided object is tagged as "Sensor"
        bool isSensor = other.CompareTag("Sensor");
        if (isSensor)
        {
            // Increment the score
            GameManager.Instance.score++;
            Debug.Log("Score: " + GameManager.Instance.score);
        }
        else
        {
            // End the game and stop movement
            _thisRigidbody.linearVelocity = Vector3.zero; // Stop all motion
            GameManager.Instance.EndGame();
        }
    }
    private void Jump()
    {
        // Reset cooldown timer for the next jump
        _jumpCooldown = jumpInterval;

        // Clear existing velocity to ensure consistent jumps
        _thisRigidbody.linearVelocity = Vector3.zero;

        // Apply upward force for the jump
        _thisRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
}