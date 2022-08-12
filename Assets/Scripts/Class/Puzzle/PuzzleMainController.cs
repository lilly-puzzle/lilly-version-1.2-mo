using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;

public class PuzzleMainController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] protected int puzzleCode = -1;

    protected void Awake() {
        if (puzzleCode == -1) {
            Debug.LogError("Error: Puzzle code is missing");
        }

        LoadEachPuzzleData();
    }

    private void Start() {
        SetupPuz();
    }

    public virtual void SaveEachPuzzleData(PuzzleData a_puzzleData) { }
    protected virtual void LoadEachPuzzleData() { }
    protected virtual void SetupPuz() { }

    protected void PuzzleClear() {
        PuzzleManager.instance.SetPuzzleClear(puzzleCode);
    }
}
