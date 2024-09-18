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
      }
      else{
         Hide();
      }
   }

   private void Update(){
      recipesDeliveredText = null; //todo waiting update
   }

   private void Show(){
      gameObject.SetActive(true);
   }

   private void Hide(){
      gameObject.SetActive(false);
   }
}
