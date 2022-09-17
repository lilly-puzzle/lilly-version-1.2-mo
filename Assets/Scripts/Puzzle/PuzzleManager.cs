using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;

public class PuzzleManager : MonoBehaviour
{
    [System.Serializable] private class PuzzlePrefabByFloor {
        public List<GameObject> puzzlePrefab;
    }

    public static PuzzleManager instance;

    [Header("Object Variables")]
    [SerializeField] private List<PuzzlePrefabByFloor> puzzlePrefabByFloor;
    private GameObject activePuzzleObject;

    [Header("PlayData Variables")]
    private int[] clearedPuzzle;
    public PuzzleData puzzleData { get; private set; }

    private void Awake() {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        LoadPuzzleData();
    }

    // save & load
    public void SavePuzzleData() {
        DataManager.instance.SetPuzzleData(clearedPuzzle, puzzleData);
    }

    private void LoadPuzzleData() {
        clearedPuzzle = DataManager.gameData.playData.clearedPuzzle;
        puzzleData = DataManager.gameData.playData.puzzleData;
    }

    private void SaveActivePuzzleData() {
        PuzzleMainController mainController = activePuzzleObject.GetComponent<PuzzleMainController>();
        mainController.SaveEachPuzzleData(puzzleData);
    }

    // start puzzle
    public void AwakePuzzle() {
        int puzzleCodeToRun = GetPuzzleCode();
        ActivateController(puzzleCodeToRun);
    }

    private int GetPuzzleCode() {
        return PlayerPrefs.GetInt("Puzzle Code", -1);
    }

    private void ActivateController(int a_puzzleCode) {
        int floorNum = a_puzzleCode / 100;
        int puzzleNum = a_puzzleCode % 100;
        
        activePuzzleObject = Instantiate(puzzlePrefabByFloor[floorNum].puzzlePrefab[puzzleNum], GameObject.Find("Puzzle").transform);
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
