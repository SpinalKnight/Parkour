using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 14f;
    public float gravity = -30f;
    public float jumpHeight = 3f;
    public int jumpI = 1;
    public float pushPower = 0.001f;
    [SerializeField] float runAcceleration = 0.5f;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float wallDistance = 1f;
    public LayerMask wallMask;
    bool touchingWall;
    public float wallJumpLeftI = 1000f;
    public float wallJumpRightI = 1000f;


    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        touchingWall = Physics.CheckSphere(GroundCheck.position, wallDistance, wallMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (touchingWall)
        {

            gravity = -13f;
            speed = 18f;
            RaycastHit leftWallHit;
            RaycastHit rightWallhit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out leftWallHit, 1, wallMask) && Input.GetButtonDown("Jump"))
            {
                jumpHeight += 4;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Vector3 pushRight = transform.right;
                wallJumpLeftI = 0;
                jumpHeight -= 4;
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out rightWallhit, 1, wallMask) && Input.GetButtonDown("Jump"))
            {
                jumpHeight += 4;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Vector3 pushLeft = -transform.right;

                wallJumpRightI = 0;
                   
                jumpHeight -= 4;

            }

        }
        else
        {
            gravity = -30f;
        }

        Vector3 pushRightI = transform.right;

        if (wallJumpLeftI < 10)
        {
            controller.Move(pushRightI * pushPower);
            wallJumpLeftI++;
        }

        Vector3 pushLeftI = -transform.right;

        if (wallJumpRightI < 10)
        {
            controller.Move(pushLeftI * pushPower);
            wallJumpRightI++;
        }


        if (isGrounded && velocity.y < 0)
        {
            jumpI = 2;
            velocity.y = -2f;
        }
        

    
        if (Input.GetButtonDown("Jump") && jumpI >= 2)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpI--;
        }
        

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
