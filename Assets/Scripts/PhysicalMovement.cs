using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicalMovement : MonoBehaviour
{
    //Referencias
    private CharacterController controller;

    [Header("Gravedad")]
    [SerializeField] private float jumpGravity = 1f;
    [SerializeField] private float fallingGravity = 4f;
    [SerializeField] private float maxFallingGravity = -10f;

    [Header("Salto")]
    [SerializeField] private int jumps = 2;
    private int jumpCount = 0;

    //Variables privadas sin categoria
    private Vector3 velocity;
    private float disableGroundDetection;
    private bool jumpIsPressed;

    //Propiedades 
    public float VerticalForce => velocity.y;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && disableGroundDetection == 0f)
        {
            velocity.y = 0;
            jumpCount = 0;
        }
        else
        {
            float gravityScale = fallingGravity;
            if (controller.velocity.y >= 0 && jumpIsPressed) gravityScale = jumpGravity;

            velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
            velocity.y = Mathf.Max(velocity.y, maxFallingGravity);

            disableGroundDetection -= Time.deltaTime;
            disableGroundDetection = Mathf.Max(0, disableGroundDetection);  
        }
    }

    public void Move(Vector3 velocity)
    {
        this.velocity.x = velocity.x; 
        this.velocity.z = velocity.z; 
    }

    public void Jump(float force)
    {  
        if (jumpCount >= jumps) return;

        velocity.y = force;
        jumpCount++;
        disableGroundDetection = 0.2f;
        jumpIsPressed = true;

    }
    public void DisableJump()
    {
        jumpIsPressed = false;
    }

}
