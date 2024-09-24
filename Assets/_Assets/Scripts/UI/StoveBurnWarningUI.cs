using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour{
    
    [SerializeField] private StoveCounter stoveCounter;

    private void Start(){
        stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        Hide();
    }

    private void StoveCounterOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.isFried() && e.progressNormalize >= burnShowProgressAmount;
        if (show){
            Show();
        }
        else{
            Hide();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }
    
    private void Hide(){
        gameObject.SetActive(false);
    }
}
