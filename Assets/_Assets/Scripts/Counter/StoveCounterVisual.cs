using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour{

    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particleGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start(){
        stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
    }

    private void StoveCounterOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e){
        if (e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying){
            Show();
        }
        else{
            Hide();
        }
    }

    private void Show(){
        stoveOnGameObject.SetActive(true);
        particleGameObject.SetActive(true);
    }

    private void Hide(){
        stoveOnGameObject.SetActive(false);
        particleGameObject.SetActive(false);
    }
}