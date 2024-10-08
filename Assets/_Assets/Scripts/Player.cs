using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public static Player Instance{ get;private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs{
        public ClearCounter selectedCounter;
    }

    //SerializeField可以在unity编辑器使用
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    public float rotationSpeed = 10f;
    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private ClearCounter selectClearCounter;

    private void Awake(){
        if (Instance != null){
            Debug.LogError("this is a singleton but find Instance already exists");
        }
        Instance = this;
    }

    private void Start(){
        gameInput.OnInteractAction += GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, EventArgs e){
        if (selectClearCounter!=null){
            selectClearCounter.Interact();
        }
    }

    void Update(){
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,
                counterLayerMask)){
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
                if (clearCounter != selectClearCounter){
                    setSelectedCounter(clearCounter);
                }
            }else{
                setSelectedCounter(null);
            }
        }else{
            setSelectedCounter(null);
        }
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);
        if (!canMove){
            //代表正在墙边
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);
            if (canMove){
                moveDir = moveDirX;
            }
            else{
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove){
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove){
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        if (inputVector != Vector2.zero){
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }
    }

    public bool IsWalking(){
        return isWalking;
    }

    private void setSelectedCounter(ClearCounter counter){
        this.selectClearCounter = counter;
        OnSelectedCounterChanged ? .Invoke(this,new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectClearCounter
        });
    }
}