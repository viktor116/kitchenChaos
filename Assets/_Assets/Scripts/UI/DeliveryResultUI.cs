using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour{

    private const string POPUP = "Popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite successIcon;
    [SerializeField] private Sprite failIcon;

    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }
    
    private void Start(){
        DeliveryManager.Instance.OnRecipeSuccess += DeliverManagerOnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliverManagerOnRecipeFail;
        Hide();
    }

    private void DeliverManagerOnRecipeSuccess(object sender, EventArgs e){
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successIcon;
        messageText.text = "DELIVERY\nFAILED";
    }
    private void DeliverManagerOnRecipeFail(object sender, EventArgs e){
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = failColor;
        iconImage.sprite = failIcon;
        messageText.text = "DELIVERY\nSUCCESS";
    }

    private void Show(){
        gameObject.SetActive(true);
    }
    
    private void Hide(){
        gameObject.SetActive(false);
    }

}