using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class DeliveryManager : MonoBehaviour{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    public static DeliveryManager Instance{ get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake(){
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f){
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax){
                RecipeSO recipeSO = recipeListSO.recipeSOList[Random.Range(0,recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(recipeSO);
                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        for (int i = 0; i < waitingRecipeSOList.Count; i++){
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count){
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList){
                    bool ingredientFount = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
                        //盘子与配方的等待SO匹配
                        if (plateKitchenObjectSO == recipeKitchenObjectSO){
                            ingredientFount = true;
                            break;
                        }
                    }
                    if (!ingredientFount){
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe){
                    //玩家配方正确   
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFail?.Invoke(this,EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipeSOList;
    }
}
