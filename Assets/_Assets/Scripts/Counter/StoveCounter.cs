using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter{

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;

    // private void Start(){
    //     StartCoroutine(HandleFryTimer());
    // }
    //
    // private IEnumerator HandleFryTimer(){
    //     yield return new WaitForSeconds(1f);
    //     Debug.Log("一秒钟过去了！");
    // }

    public override void Interact(Player player){
        if (!HasKitchenObject()){
            if (player.HasKitchenObject()){
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){ //if there in recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                 
                }
            }
        }
        else{
            if (!player.HasKitchenObject()){
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo){
        FryingRecipeSO fryingRecipeSo = GetCuttingRecipeSOWithInput(inputKitchenObjectSo);
        return fryingRecipeSo != null ? fryingRecipeSo.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSo = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSo != null;
    }

    private FryingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOs){
            if (fryingRecipeSO.input == inputKitchenObjectSo){
                return fryingRecipeSO;
            }
        }
        return null;
    }
}
