using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float movementSpeed = 10f;
    public float walkSpeed = 20f;
    public float runSpeed = 40f;
    public float jumpHeight = 10f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;
    float turnSoothVelocity;
    
    Vector3 velocity;

    public bool isGrounded;
    // isGround everthing that is on the Ground Layer can work.
    public bool Sprinting;

    void Start()
    {

    }
    void Update()
    {

        //This help so the game know when that character is on the ground.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //This help when the character is on the ground the celocity restart.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Character Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Character Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        
        //Turn on and off Sprinting.
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            Sprinting = true;
        }
        else
        {
            Sprinting = false;
        }

        if (Sprinting == true)
        {
            movementSpeed = runSpeed;
        }

        if (Sprinting == false)
        {
            movementSpeed = walkSpeed;
        }
        
        /*
        // Turn on Sprinting.
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            Sprinting = true;
            //Debug.Log("Run");
        }

        // Turn off Sprinting.
        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            Sprinting = false;
        }
        */

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
        }

        //This is the gracity so the character can fall.
        velocity.y += gravity * Time.deltaTime;

        //This the velocity.
        controller.Move(velocity * Time.deltaTime);
    }
}
