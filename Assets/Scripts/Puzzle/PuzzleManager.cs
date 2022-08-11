using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    [Header("Variables")]
    private bool isChanged = false;

    // setter
    public void SetIsChanged(bool a_isChanged) { isChanged = a_isChanged; }

    [Header("PlayData Variables")]
    private int[] clearedPuzzle;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        LoadPuzzleData();
    }

    // save & load
    public void SavePuzzleData() {
        if (isChanged) {
            DataManager.instance.SetPuzzleData(clearedPuzzle);
        }
    }

    private void LoadPuzzleData() {
        clearedPuzzle = DataManager.gameData.playData.clearedPuzzle;
    }

    // start puzzle
    public void AwakePuzzle() {

    }

    // about puzzle clear
    public bool CheckIfPuzzleClear(int a_puzzleCode) {
        int floorNum = a_puzzleCode / 100;
        int puzzleNum = a_puzzleCode % 100;

        if (((clearedPuzzle[floorNum] >> puzzleNum) & 1) == 1) return true;
        else return false;
    }

    public bool SetPuzzleClear(int a_puzzleCode) {
        int floorNum = a_puzzleCode / 100;
        int puzzleNum = a_puzzleCode % 100;

        clearedPuzzle[floorNum] |= 1 << puzzleNum;

        return CheckIfPuzzleClear(a_puzzleCode);
    }
}
