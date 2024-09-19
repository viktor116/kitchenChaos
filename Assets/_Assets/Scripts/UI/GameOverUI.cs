using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour{
   [SerializeField] private TextMeshProUGUI recipesDeliveredText;
   
   private void Start(){
      KitchenGameManager.Instance.OnStateChanged += KitchenGameManagerOnStateChanged;
      Hide();
   }

   private void KitchenGameManagerOnStateChanged(object sender, EventArgs e){
      if (KitchenGameManager.Instance.IsGameOver()){
         Show();
         recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString(); //todo waiting update
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
