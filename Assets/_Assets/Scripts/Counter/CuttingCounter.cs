using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter{

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs{
        public float progressNormalize;
    }

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;

    private int cuttingProgress;
    
    public override void Interact(Player player){
        if (!HasKitchenObject()){
            if (player.HasKitchenObject()){
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){ //if there in recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs{
                        progressNormalize = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                    
                }
            }
        }
        else{
            if (!player.HasKitchenObject()){
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player){
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){ //hava kitchen object in here
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnCut?.Invoke(this,EventArgs.Empty);
            OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs{
                progressNormalize = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){
                KitchenObjectSO kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSo);
        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo){
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOs){
            if (cuttingRecipeSO.input == inputKitchenObjectSo){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}