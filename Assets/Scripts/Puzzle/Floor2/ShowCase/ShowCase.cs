using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor2;

public class ShowCase : PuzzleMainController
{
    private bool scItemAct;
    private bool scLeftDoorOpen;
    private bool scRightDoorOpen;

    [SerializeField] private GameObject[] scDoorObject = new GameObject[2]; // [0] == Left  [1] == Right
    private SCDoor[] scDoor = new SCDoor[2];   // [0] == Left  [1] == Right
    [SerializeField] private GameObject scItemObject;
    private SCItem scItem;

    protected override void LoadEachPuzzleData() {
        ShowCaseData saveData = PuzzleManager.instance.puzzleData.floor2Data.saveShowCase;

        scItemAct = saveData.scItemAct;
        scLeftDoorOpen = saveData.scLeftDoorOpen;
        scRightDoorOpen = saveData.scRightDoorOpen;
    }

    public override void SaveEachPuzzleData(PuzzleData a_puzzleSaveData) {
        a_puzzleSaveData.floor2Data.saveShowCase.scItemAct = scItemAct;
        a_puzzleSaveData.floor2Data.saveShowCase.scLeftDoorOpen = scLeftDoorOpen;
        a_puzzleSaveData.floor2Data.saveShowCase.scRightDoorOpen = scRightDoorOpen;
    }

    protected override void SetupPuz(){
        scDoor[0] = scDoorObject[0].GetComponent<SCDoor>();
        scDoor[1] = scDoorObject[1].GetComponent<SCDoor>();
        scItem = scItemObject.GetComponent<SCItem>();
        DoorInit();
        ItemInit();
    }

    private void DoorInit(){
        if(scLeftDoorOpen){
            scDoor[0].Open();
        }
        else{
            scDoor[0].Close();
        }

        if(scRightDoorOpen){
            scDoor[1].Open();
        }
        else{
            scDoor[1].Close();
        }
    }
    
    private void ItemInit(){
        if(scItemAct){
            scItemObject.SetActive(false);
        }
        else{
            scItemObject.SetActive(true);
        }
    }

    public void SetDoorOpen(bool isLeft, bool isOpen){
        if(isLeft){
            if(isOpen){
                scLeftDoorOpen = true;
            }
            else{
                scLeftDoorOpen = false;
            }
        }
        else{
            if(isOpen){
                scRightDoorOpen = true;
            }
            else{
                scRightDoorOpen = false;
            }
        }
    }

    public bool GetDoorOpen(bool isLeft){
        if(isLeft){
            return scLeftDoorOpen;
        }
        else{
            return scRightDoorOpen;
        }
    }

    public void SetItemAct(bool itemAct){
        if(itemAct){
            itemAct = true;
        }
        else{
            itemAct = false;
        }
    }
}
