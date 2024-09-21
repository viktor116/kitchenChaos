using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake(){
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionButton.onClick.AddListener(() => {
            Hide();
            OptionUI.Instance.Show(Show);
        });
    }

    private void Start(){
        KitchenGameManager.Instance.OnGamePaused += GameManagerOnGamePaused;
        KitchenGameManager.Instance.OnGameUnPaused += GameManagerOnGameUnPaused;
        Hide();
    }

    private void GameManagerOnGameUnPaused(object sender, EventArgs e){
        Hide();
    }

    private void GameManagerOnGamePaused(object sender, EventArgs e){
        Show();
    }

    private void Show(){
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

}
