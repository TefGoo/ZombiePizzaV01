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
    private bool hasBox;
    private float originalWalkSpeed;

    private Animator animator; // Reference to the Animator component

    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";
    private const string IS_CROUCHING = "IsCrouching";
    private const string IS_ATTACKING = "IsAttacking";

    private bool isUIButtonPressed = false; // New boolean variable to track UI button press
    private GameObject objectToShowHide; // Reference to the object to show and hide

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        // Find and store the reference to the object you want to show and hide
        objectToShowHide = GameObject.Find("OrderList");
        // Alternatively, if the object is a child of the player, you can access it like this:
        // objectToShowHide = transform.Find("YourObjectToShowHideName").gameObject;

        // Disable the object at the start (assuming you want it hidden initially)
        if (objectToShowHide != null)
        {
            objectToShowHide.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the UI button is pressed
        if (!isUIButtonPressed)
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
                if (!hasBox) // Only allow attacking if not holding a box
                {
                    isAttacking = true;
                    originalWalkSpeed = currentSpeed; // Store the original walk speed before attacking
                    currentSpeed = currentSpeed / 2f; // Reduce speed in half while attacking
                    Debug.Log("Attack animation started");
                }
            }
            if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("ControllerAttack"))
            {
                isAttacking = false;
                currentSpeed = isCrouching ? crouchSpeed : originalWalkSpeed; // Restore original walk speed after attacking
                Debug.Log("Attack animation stopped");
            }

            // Check if the "1" key is pressed to show/hide the object
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ToggleObjectVisibility();
            }
        }

        // Update the animator only if not pressing the UI button
        if (!isUIButtonPressed)
        {
            UpdateAnimator();
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool(IS_WALKING, isWalking);
        animator.SetBool(IS_RUNNING, isRunning && !isCrouching);
        animator.SetBool(IS_ATTACKING, isAttacking && !hasBox);
        animator.SetBool(IS_CROUCHING, isCrouching);
        animator.SetBool("HasBox", hasBox);
    }

    // Method to toggle the visibility of the object
    private void ToggleObjectVisibility()
    {
        if (objectToShowHide != null)
        {
            // Toggle the object's active state to show/hide it
            objectToShowHide.SetActive(!objectToShowHide.activeSelf);
        }
    }

    // Method to set the UI button press state (call this method from your UI button's click event)
    public void SetUIButtonPressed(bool value)
    {
        isUIButtonPressed = value;
    }

    public void SetHasBox(bool value)
    {
        hasBox = value;
    }

    public bool HasBox()
    {
        return hasBox;
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
        return isAttacking && !hasBox;
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }

}
