using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter{

    [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player){
        if (!HasKitchenObject()){ //柜台上没有东西
            if (player.HasKitchenObject()){
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else{
            if (!player.HasKitchenObject()){
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}