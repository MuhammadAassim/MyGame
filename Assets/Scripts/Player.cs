using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private GameInputs gameInputs;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody rb;

    [Header("Palyer Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotSpeed;

    [Header("Camera")]
    [SerializeField] private Camera playerCam;

    private Vector3 moveDir = Vector3.zero;

    private bool isWalking;




    private void FixedUpdate()
    {
        Movement();
        LookAt();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void Movement()
    {
        Vector3 inputMoveDir = gameInputs.GetMovementVector();

        Vector3 cameraForward = GetCameraForward(playerCam);
        Vector3 cameraRight = GetCameraRight(playerCam);

        moveDir = (inputMoveDir.x * cameraRight + inputMoveDir.y * cameraForward).normalized;

        isWalking = moveDir != Vector3.zero;

        moveDir.y = 0;
        

        rb.AddForce(moveDir, ForceMode.Impulse);
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        
        moveDir = Vector3.zero;

        

    }


    private void LookAt()
    {
        if (gameInputs.GetMovementVector().sqrMagnitude > 0.1f)
        {
            Vector3 tragetDirection = rb.velocity.normalized;
            if(tragetDirection.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(tragetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            } 
        }

    }

    private Vector3 GetCameraRight(Camera playerCam)
    {
        Vector3 right = playerCam.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCameraForward(Camera playerCam)
    {
        Vector3 forward = playerCam.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
}
