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
            else{ // 如果手上有东西
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //玩家手上拿着盘子
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                }
                else{ //玩家手上拿着物品
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
}