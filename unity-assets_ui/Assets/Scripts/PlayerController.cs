using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInputs playerInputs;
    private Vector3 moveDirection;
    private bool isJumping;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.PlayerActionMap.Enable();
        playerInputs.PlayerActionMap.Jump.started += ctx => isJumping = true;
        playerInputs.PlayerActionMap.Jump.canceled += ctx => isJumping = false;
    }

    private void OnDisable()
    {
        playerInputs.PlayerActionMap.Disable();
    }

    private void Update()
    {
        ApplyMovement();
        ApplyVerticalMovement();
        CheckForPlayerDeath();
    }

    private void ApplyMovement()
    {
        Vector2 xzMoveDir = playerInputs.PlayerActionMap.Move.ReadValue<Vector2>();
        moveDirection = transform.forward * xzMoveDir.y + transform.right * xzMoveDir.x + transform.up * moveDirection.y;
        controller.Move(moveDirection * Time.deltaTime * moveSpeed);
    }

    private void ApplyVerticalMovement()
    {
        if (controller.isGrounded)
        {
            if (isJumping)
            {
                moveDirection.y = jumpHeight;
                isJumping = false;
            }
            else
            {
                moveDirection.y = 0;
            }
        }
        else
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    private void CheckForPlayerDeath()
    {
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 50, 0);
        }
    }
}
