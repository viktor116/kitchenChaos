using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour{

    public static GameInput Instance{ get ; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnAccelerate;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;

    public enum Binding{
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
    }

    private void Awake(){
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += InteractOnPerformed; //添加事件
        playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
        playerInputActions.Player.Accelerate.performed += AccelerateOnPerformed;
        playerInputActions.Player.Pause.performed += PauseOnPerformed;
    }
    
    private void OnDestroy(){
        playerInputActions.Player.Interact.performed -= InteractOnPerformed; //减少事件
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternateOnPerformed;
        playerInputActions.Player.Accelerate.performed -= AccelerateOnPerformed;
        playerInputActions.Player.Pause.performed -= PauseOnPerformed;
        
        playerInputActions.Dispose();
    }

    private void PauseOnPerformed(InputAction.CallbackContext obj){
        OnPauseAction?.Invoke(this,EventArgs.Empty);
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

    public string GetBindingText(Binding binding){
        switch (binding){
            case Binding.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            default:
                return "error";
        }
    }
    
    public void ReBingBinding(Binding binding,Action onActionRebound){
        playerInputActions.Player.Disable();
        InputAction inputAction;
        int bindingIndex;
        switch (binding){
            default:
            case Binding.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }
        
        
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => {
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
        }).Start();
    }
}
