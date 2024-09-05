using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress{
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs{
        public State state;
    }

    public enum State{
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;
    [SerializeField] private BurningRecipeSO[] burningRecipeSos;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start(){
        state = State.Idle;
    }

    private void Update(){
        if (HasKitchenObject()){
            switch (state){
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged? .Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalize = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax){
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged? .Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalize = burningTimer / burningRecipeSO.burningTimerMax
                    });
                    if (burningTimer > burningRecipeSO.burningTimerMax){
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                            state = state
                        });
                        OnProgressChanged? .Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalize = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player){
        if (!HasKitchenObject()){
            if (player.HasKitchenObject()){
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){ //if there in recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    //set fryingRecipeSO and set state and reset frying timer
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalize = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }else if(HasBurningRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    state = State.Fried;
                    burningTimer = 0f;
                    OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalize = burningTimer / burningRecipeSO.burningTimerMax
                    });
                }
            }
        }
        else{
            if (!player.HasKitchenObject()){
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                fryingTimer = 0f;
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                    state = state
                });
                OnProgressChanged? .Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalize = 0f
                });
            }
            else{ //玩家手上有物品
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();   
                        state = State.Idle;
                        fryingTimer = 0f;
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                            state = state
                        });
                        OnProgressChanged? .Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalize = 0f
                        });
                    }
                }
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo){
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSo);
        return fryingRecipeSo != null ? fryingRecipeSo.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSo != null;
    }
    private bool HasBurningRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        BurningRecipeSO burningRecipeSO = GetBurningRecipeSOWithInput(inputKitchenObjectSO);
        return burningRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOs){
            if (fryingRecipeSO.input == inputKitchenObjectSo){
                return fryingRecipeSO;
            }
        }
        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo){
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSos){
            if (burningRecipeSO.input == inputKitchenObjectSo){
                return burningRecipeSO;
            }
        }
        return null;
    }
    
}