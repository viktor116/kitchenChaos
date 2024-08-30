using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO;
    }

    public void setClearCounter(ClearCounter clearCounter){
        this.clearCounter = clearCounter;
    }

    public ClearCounter getClearCounter(){
        return this.clearCounter;
    }

}