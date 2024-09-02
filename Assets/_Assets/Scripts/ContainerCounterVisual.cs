using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour{

    private const string OPEN_CLOSE = "OpenClose";
    
    [SerializeField] private ContainCounter containCounter;

    private Animator animator;
        
    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Start(){
        containCounter.OnPlayerGrabbedObject += ContainCounterOnPlayerGrabbedObject;
    }

    private void ContainCounterOnPlayerGrabbedObject(object sender, EventArgs e){
        Debug.Log("tragger");
        animator.SetTrigger(OPEN_CLOSE);
    }
}
 