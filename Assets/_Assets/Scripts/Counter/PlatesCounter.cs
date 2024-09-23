using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter{

    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved; 

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update(){
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax){
            spawnPlateTimer = 0f;
            if (KitchenGameManager.Instance.IsGamePlaying() && platesSpawnAmount < platesSpawnedAmountMax){
                platesSpawnAmount++;
                OnPlateSpawn?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player){
        if (!player.HasKitchenObject()){ // 无item
            if (platesSpawnAmount > 0){
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved? .Invoke(this,EventArgs.Empty);
            }
        }
    }
}