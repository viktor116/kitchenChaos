using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContainCounter : BaseCounter{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    
    public event EventHandler OnPlayerGrabbedObject;
    public override void Interact(Player player){
        if (!player.HasKitchenObject()){
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
            OnPlayerGrabbedObject? .Invoke(this,EventArgs.Empty);
        }
    }
    
}