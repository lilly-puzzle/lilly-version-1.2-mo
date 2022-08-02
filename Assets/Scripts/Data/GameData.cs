using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;

public class GameData
{
    public bool newGame;
    public SettingData settingData;
    public CharacterData characterData;
    public PlayData playData;

    public GameData() {
        newGame = true;
        settingData = new SettingData();
        characterData = new CharacterData();
        playData = new PlayData();
    }

    public string ToJson() {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json) {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}
