using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{

    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start(){
        cuttingCounter.OnProgressChanged += CuttingCounterOnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounterOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e){
        barImage.fillAmount = e.progressNormalize;
        if (barImage.fillAmount == 1f || barImage.fillAmount == 0){
            Hide();
        }
        else{
            Show();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
