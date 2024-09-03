using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour{

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnAccelerate;

    private PlayerInputActions playerInputActions;
    
    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += InteractOnPerformed; //添加事件
        playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
        playerInputActions.Player.Accelerate.performed += AccelerateOnPerformed;
    }

    private void AccelerateOnPerformed(InputAction.CallbackContext obj){
        OnAccelerate?.Invoke(this,EventArgs.Empty);
    }

    private void InteractAlternateOnPerformed(InputAction.CallbackContext obj){
        OnInteractAlternateAction ? .Invoke(this,EventArgs.Empty); 
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        // 归一化向量 不会超过1
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
