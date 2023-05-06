using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    private bool isWalking;
    private bool isRunning;
    private bool isAttacking;
    private bool isCrouching;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
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

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        isRunning = Input.GetKey(KeyCode.LeftShift);

        isCrouching = Input.GetKey(KeyCode.C);

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            // Trigger attack animation here
            Debug.Log("Attack animation started");
        }
        if (Input.GetMouseButtonUp(0))
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
