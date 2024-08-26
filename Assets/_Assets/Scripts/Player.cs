using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    //SerializeField可以在unity编辑器使用
    [SerializeField] private float moveSpeed = 5f;
    public float accelerate = 1.5f;
    void Update(){
        Vector2 inputVector = new Vector2(0, 0);
        float mos = moveSpeed;
        if (Input.GetKey(KeyCode.W)){
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S)){
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)){
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)){
            inputVector.x = +1;
        }
        if (Input.GetKey(KeyCode.LeftShift)){
            mos = moveSpeed * accelerate;
        }
        // 归一化向量 不会超过1
        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * Time.deltaTime * mos;
        Debug.Log(inputVector);
    }
}