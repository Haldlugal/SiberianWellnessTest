using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public Animator animator;
    public Transform pivot;    
    public GameObject playerModel;

    private CharacterController controller;
    private Vector3 moveDirection;
    private string movementType = "thirdPerson";
    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.paused)
        {
            handleMovementType();
            handleMovement();            
            handleAnimation();
        }
    }   

    private void handleMovementType()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (movementType == "thirdPerson")
            {
                movementType = "topDown";
            }
            else if (movementType == "topDown")
            {
                movementType = "thirdPerson";
            }
        }
    }

    private void handleMovement()
    {
        float yStore = moveDirection.y;
        switch (movementType)
        {
            case "thirdPerson":
                {
                    moveDirection = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"));
                    break;
                }
            case "topDown":
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);                    
                    break;
                }
        }

        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + Physics.gravity.y * Time.deltaTime * gravityScale;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(moveDirection.x, pivot.rotation.eulerAngles.y, moveDirection.z);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = newRotation;
        }
    }

    private void handleAnimation()
    {
        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
}
