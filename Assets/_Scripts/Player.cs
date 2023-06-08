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
    private bool crouchButtonPressed;

    private Animator animator; // Reference to the Animator component

    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";
    private const string IS_CROUCHING = "IsCrouching";
    private const string IS_ATTACKING = "IsAttacking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
            isRunning = false; // Block running while crouching
        }
        else if (isRunning)
        {
            currentSpeed = runSpeed;
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouchButtonPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouchButtonPressed = false;
        }

        // Controller input
        if (Input.GetButtonDown("ControllerCrouch"))
        {
            crouchButtonPressed = true;
        }
        if (Input.GetButtonUp("ControllerCrouch"))
        {
            crouchButtonPressed = false;
        }

        bool previousCrouchState = isCrouching;
        isCrouching = crouchButtonPressed;

        if (previousCrouchState != isCrouching && animator != null)
        {
            animator.SetBool(IS_CROUCHING, isCrouching);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("ControllerAttack"))
        {
            isAttacking = true;
            Debug.Log("Attack animation started");
        }
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("ControllerAttack"))
        {
            isAttacking = false;
            Debug.Log("Attack animation stopped");
        }

        if (animator != null)
        {
            UpdateAnimator();
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool(IS_WALKING, isWalking);
        animator.SetBool(IS_RUNNING, isRunning && !isCrouching);
        animator.SetBool(IS_ATTACKING, isAttacking);
        animator.SetBool(IS_CROUCHING, isCrouching);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsRunning()
    {
        return isRunning && !isCrouching;
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
