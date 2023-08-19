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
    private bool hasGun;
    private float originalWalkSpeed;

    private Animator animator;

    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";
    private const string IS_CROUCHING = "IsCrouching";
    private const string IS_ATTACKING = "IsAttacking";

    private bool isUIButtonPressed = false;
    private GameObject objectToShowHide;
    public GameObject objectToHide; // Reference to the GameObject you want to hide/show

    private GameObject objectToShowOnAttack;
    private float activationTime;
    private bool isObjectActivated;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        objectToShowHide = GameObject.Find("OrderList");

        if (objectToShowHide != null)
        {
            objectToShowHide.SetActive(false);
        }

        objectToShowOnAttack = GameObject.Find("AttackCollider");

        if (objectToShowOnAttack != null)
        {
            objectToShowOnAttack.SetActive(false);
        }
    }

    private void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
            isRunning = false;
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

        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetButton("ControllerRun");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouchButtonPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouchButtonPressed = false;
        }

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
            originalWalkSpeed = currentSpeed;

            // Activate the object to show on attack
            objectToShowOnAttack.SetActive(true);
            activationTime = Time.time;
            isObjectActivated = true;

            currentSpeed = currentSpeed / 2f;
            Debug.Log("Attack animation started");
        }
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("ControllerAttack"))
        {
            isAttacking = false;
            currentSpeed = isCrouching ? crouchSpeed : originalWalkSpeed;
            Debug.Log("Attack animation stopped");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleObjectVisibility();
        }

        if (!isUIButtonPressed)
        {
            UpdateAnimator();
        }

        // Check if the object has been active for 2 seconds and deactivate it
        if (isObjectActivated && Time.time - activationTime >= 2f)
        {
            objectToShowOnAttack.SetActive(false);
            isObjectActivated = false;
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool(IS_WALKING, isWalking);
        animator.SetBool(IS_RUNNING, isRunning && !isCrouching);
        animator.SetBool(IS_ATTACKING, isAttacking);
        animator.SetBool(IS_CROUCHING, isCrouching);
        animator.SetBool("HasBox", hasBox);
        animator.SetBool("HasGun", hasGun);
    }

    private void ToggleObjectVisibility()
    {
        if (objectToShowHide != null && objectToHide != null)
        {
            bool objectToShowActive = objectToShowHide.activeSelf;

            objectToShowHide.SetActive(!objectToShowActive);
            objectToHide.SetActive(objectToShowActive);
        }
    }


    public void SetUIButtonPressed(bool value)
    {
        isUIButtonPressed = value;
    }

    public void SetHasBox(bool value)
    {
        hasBox = value;
    }

    public void SetHasGun(bool value)
    {
        hasGun = value;
    }

    public bool HasBox()
    {
        return hasBox;
    }

    public bool HasGun()
    {
        return hasGun;
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
