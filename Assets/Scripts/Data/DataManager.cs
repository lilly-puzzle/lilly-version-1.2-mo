using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public static GameData gameData { get; private set; }

    private void Awake() {
        instance = this;

        gameData = new GameData();
        LoadDataFromJson();
    }

    // If game is quited..
    private void OnApplicationQuit() {
        if (gameData != null) SaveDataInJson();
        Debug.Log("Quit Game");
    }

    // Save & Load
    private void SaveDataInJson() {
        if (FileManager.WriteToFile("GameData.dat", gameData.ToJson())) {
            Debug.Log("Save successful");
        }
        // else {
        //     SaveDataInJson();
        // }
    }

    private void LoadDataFromJson() {
        int result = FileManager.LoadFromFile("GameData.dat", out var json);
        
        if (result == 1) {
            gameData.LoadFromJson(json);
        } else if (result == -1) {
            LoadDataFromJson();
        }
    }

    // Reset
    public void ResetGame() {
        gameData = new GameData();
    }

    // Setter
    public void SetNewGame(bool a_newGame) {
        gameData.newGame = a_newGame;
    }

    public void SetSoundSetting(bool a_isMute, int a_masterVolume, int a_bgmVolume, int a_effectVolume) {
        gameData.settingData.soundSetting.isMute = a_isMute;
        gameData.settingData.soundSetting.masterVolume = a_masterVolume;
        gameData.settingData.soundSetting.bgmVolume = a_bgmVolume;
        gameData.settingData.soundSetting.effectVolume = a_effectVolume;
    }
}
