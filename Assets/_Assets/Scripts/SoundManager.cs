using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour{

    private const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance{ get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake(){
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, 1f);
    }

    private void Start(){
        DeliveryManager.Instance.OnRecipeSuccess += DeliverManagerOnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManagerOnRecipeFail;
        CuttingCounter.OnAnyCut += CuttingCounterOnAnyCut;
        Player.Instance.OnPickSomething += InstanceOnPickSomething;
        BaseCounter.OnAnyObjectPlaceHere += BaseCounterOnAnyObjectPlaceHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounterOnAnyObjectTrashed;
    }

    private void TrashCounterOnAnyObjectTrashed(object sender, EventArgs e){
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash,trashCounter.transform.position,5);
    }

    private void BaseCounterOnAnyObjectPlaceHere(object sender, EventArgs e){
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop,baseCounter.transform.position,5);
    }

    private void InstanceOnPickSomething(object sender, EventArgs e){
        PlaySound(audioClipRefsSO.objectPickup,Player.Instance.transform.position,5);
    }

    private void CuttingCounterOnAnyCut(object sender, EventArgs e){
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop,cuttingCounter.transform.position,5);
    }

    private void DeliveryManagerOnRecipeFail(object sender, EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverFail,deliveryCounter.transform.position,5);
    }

    private void DeliverManagerOnRecipeSuccess(object sender, EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverSuccess,deliveryCounter.transform.position,5);
    }
    
    public void PlaySound(AudioClip[] audioClipArray,Vector3 position,float volumeMultiplier = 1f){
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volumeMultiplier);
    }

    public void PlaySound(AudioClip audioClip,Vector3 position,float volumeMultiplier = 1f){
        AudioSource.PlayClipAtPoint(audioClip,position,volumeMultiplier * volume);
    }

    public void PlayFootStepsSound(Vector3 position, float volume = 1f){
        PlaySound(audioClipRefsSO.footstep,position,volume);
    }

    public void ChangeVolume(){
        volume += .1f;
        if (volume > 1f){
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME,volume);
        PlayerPrefs.Save();
    }

    public float GetVolume(){
        return volume;
    }
}
