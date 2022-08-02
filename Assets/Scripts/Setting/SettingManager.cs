using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;

    [Header("Variable")]
    [SerializeField] private Sprite[] muteSprites = new Sprite[2];   // [0] Unmute [1] Mute
    private bool isMute;

    [Header("UI Object")]
    [SerializeField] private GameObject settingCanvas;
    [SerializeField] private GameObject muteToggle;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    private void Awake(){
        instance = this;
    }

    private void Start(){
        LoadMute();
        LoadSlider();
    }

    public void OpenSetting(){
        settingCanvas.SetActive(true);
    }

    public void CloseSetting(){
        SaveSetting();
        settingCanvas.SetActive(false);
    }

    private void LoadMute(){
        isMute = DataManager.gameData.settingData.soundSetting.isMute;
        if(isMute){
            muteToggle.GetComponent<Image>().sprite = muteSprites[1];
        }
        else{
            muteToggle.GetComponent<Image>().sprite = muteSprites[0];
        }
    }

    private void LoadSlider(){
        masterSlider.value = DataManager.gameData.settingData.soundSetting.masterVolume / (float)100;
        bgmSlider.value = DataManager.gameData.settingData.soundSetting.bgmVolume / (float)100;
        effectSlider.value = DataManager.gameData.settingData.soundSetting.effectVolume / (float)100;

        masterSlider.onValueChanged.AddListener(delegate{ApplyVolumeChange();});
        bgmSlider.onValueChanged.AddListener(delegate{ApplyVolumeChange();});
        effectSlider.onValueChanged.AddListener(delegate{ApplyVolumeChange();});
    }

    private void SaveSetting() {
        int masterVolume = (int)(masterSlider.value * 100);
        int bgmVolume = (int)(bgmSlider.value * 100);
        int effectVolume = (int)(effectSlider.value * 100);

        DataManager.instance.SetSoundSetting(isMute, masterVolume, bgmVolume, effectVolume);
    }

    private void ApplyVolumeChange(){
        //SoundManager.masterVolume = (int)(masterSlider.value * 100);
        //SoundManager.bgmVolume = (int)(bgmSlider.value * 100);
        //SoundManager.effectVolume = (int)(effectSlider.value * 100);
    }

    public void ToggleMute(){
        isMute = !isMute;
        if(isMute){
            muteToggle.GetComponent<Image>().sprite = muteSprites[1];
        }
        else{
            muteToggle.GetComponent<Image>().sprite = muteSprites[0];
        }
    }
}
