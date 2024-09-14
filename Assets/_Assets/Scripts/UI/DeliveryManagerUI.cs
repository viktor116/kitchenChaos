using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake(){
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start(){
        DeliveryManager.Instance.OnRecipeSpawned += DeliverManagerOnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManagerOnRecipeCompleted;
        UpdateVisual();
    }
    
    private void DeliverManagerOnRecipeSpawned(object sender, EventArgs e){
        UpdateVisual();
    }

    private void DeliveryManagerOnRecipeCompleted(object sender, EventArgs e){
        UpdateVisual();
    }

    private void UpdateVisual(){
        foreach (Transform child in container){
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()){
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
            // var find = recipeTransform.Find("RecipeNameText");
        }
    }
}