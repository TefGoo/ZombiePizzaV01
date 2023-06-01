using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float crouchSpeed = 3f;

    private float currentSpeed;
    private bool isWalking;
    private bool isRunning;
    private bool isAttacking;
    private bool isCrouching;

    private void Update()
    {
        // Keyboard/Mouse input
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Controller input
        Vector2 controllerInputVector = new Vector2(Input.GetAxis("ControllerHorizontal"), Input.GetAxis("ControllerVertical"));
        if (controllerInputVector.magnitude > inputVector.magnitude)
        {
            inputVector = controllerInputVector;
        }

        if (!isAttacking)
        {
            inputVector = inputVector.normalized;
        }
        else
        {
            inputVector = Vector2.zero;
        }

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Apply speed based on movement state
        if (isRunning)
        {
            currentSpeed = runSpeed;
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        transform.position += moveDir * currentSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

        // Keyboard/Mouse input
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Controller input
        isRunning = isRunning || Input.GetButton("ControllerRun");

        // Keyboard/Mouse input
        isCrouching = Input.GetKey(KeyCode.C);

        // Controller input
        isCrouching = isCrouching || Input.GetButton("ControllerCrouch");

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("ControllerAttack"))
        {
            isAttacking = true;
            // Trigger attack animation here
            Debug.Log("Attack animation started");
        }
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("ControllerAttack"))
        {
            isAttacking = false;
            // Stop attack animation here
            Debug.Log("Attack animation stopped");
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }
}
