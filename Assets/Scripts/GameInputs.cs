using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{

    private PlayerInputs playerInputs;

    private Vector2 inputVector;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
    }

    public Vector2 GetMovementVector()
    {
        inputVector = playerInputs.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
