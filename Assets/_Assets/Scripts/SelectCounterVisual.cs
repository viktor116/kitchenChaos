using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounterVisual : MonoBehaviour{
    
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    void Start(){
        Player.Instance.OnSelectedCounterChanged += PlayerSelectedCounterChanged;
    }

    private void PlayerSelectedCounterChanged(object sender,Player.OnSelectedCounterChangedEventArgs args){
        if (clearCounter == args.selectedCounter){
            Show();
        }
        else{
            Hide();
        }
    }

    private void Show(){
        visualGameObject.SetActive(true);
    }

    private void Hide(){
        visualGameObject.SetActive(false);
    }
}
    