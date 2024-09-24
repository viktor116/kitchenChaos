using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StoveBurnFlashBarUI : MonoBehaviour{

    private const string IS_FLASHING = "IsFlashing";
    
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Start(){
        stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        animator.SetBool(IS_FLASHING,false);
    }

    private void StoveCounterOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.isFried() && e.progressNormalize >= burnShowProgressAmount;
        animator.SetBool(IS_FLASHING,show);
    }
}
