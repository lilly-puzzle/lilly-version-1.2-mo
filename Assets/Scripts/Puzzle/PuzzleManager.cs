using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Variables")]
    private bool isChanged = false;

    // setter
    public void SetIsChanged(bool a_isChanged) { isChanged = a_isChanged; }

    [Header("PlayData Variables")]
    private int[] clearedPuzzle;

    private void Start() {
        LoadPuzzleData();
    }

    public void SavePuzzleData() {
        if (isChanged) {
            DataManager.instance.SetPuzzleData(clearedPuzzle);
        }
    }

    private void LoadPuzzleData() {
        clearedPuzzle = DataManager.gameData.playData.clearedPuzzle;
    }
}
