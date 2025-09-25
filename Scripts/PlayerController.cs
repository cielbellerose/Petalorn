using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 2; 
    public float gravity = 9.81f;
    public float airControl = 10; 
    public float speakDelay = 5f;
    CharacterController controller;
    Vector3 input, moveDirection;
    bool canSpeak = true;
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= speakDelay)
        {
            canSpeak = true;
        }
        else
        {
            canSpeak = false;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //make it local to the player
        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= moveSpeed;

        if(canSpeak)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                FindObjectOfType<robotBehavior>().Speak();
                elapsedTime = 0.0f;
            }
        }
        
        if (controller.isGrounded)
        {
            moveDirection = input;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                input *= sprintSpeed;
            }
            else
            {
                input *= moveSpeed; 
            }
        }
       

        if (controller.isGrounded)
        {
            moveDirection = input;
           
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        //gravity moves player down
        moveDirection.y -= gravity * Time.deltaTime;

        //move the player
        controller.Move(moveDirection * Time.deltaTime);

    }
}
