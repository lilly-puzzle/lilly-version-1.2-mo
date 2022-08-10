using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject lockedPuzzleObject;
    [SerializeField] private int[] NeedPuzzleCodes;
    private int playerOnFloor;

    void Awake(){
        lockedPuzzleObject.SetActive(false);
        playerOnFloor = DataManager.gameData.characterData.characterPos.curFloor;
        ActivateLockedPuzzle();
    }

    private void ActivateLockedPuzzle() {
        bool needPuzzleClear = true;
        for(int puzzleNum = 0; puzzleNum < NeedPuzzleCodes.Length; puzzleNum++){
            if(!PuzzleManager.instance.CheckIfPuzzleClear(NeedPuzzleCodes[puzzleNum])){
                needPuzzleClear = false;
            }
        }
        if(needPuzzleClear){
            lockedPuzzleObject.SetActive(true);
        }
    }   
}
