using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter{

    public event EventHandler OnPlateSpawn;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update(){
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax){
            spawnPlateTimer = 0f;
            if (platesSpawnAmount < platesSpawnedAmountMax){
                platesSpawnAmount++;
                OnPlateSpawn?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}