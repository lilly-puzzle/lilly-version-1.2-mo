using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor2;

public class KitchenTable : PuzzleMainController
{
    [Header("Constant Variable")]
    private int[] ktCorrectDish = new int[4] { 1, 2, 3, 4};
    private int[] ktCorrectSoup = new int[4] { 3, 2, 4, 5};
    
    [Header("Variable")]
    private int[] ktDishOnTable; // [0] LeftTop [1] RightTop [2] LeftBottom [3] RightBottom  0 == None 1 == CecillyDish 2 == LillyDish 3 == MomDish 4 == DadDish
    private int[] ktSoupOnTable; // [0] LeftTop [1] RightTop [2] LeftBottom [3] RightBottom  0 == None 1 ~ 5 Soup Magnitude
    private bool[] ktPoisionOnTable; // [0] LeftTop [1] RightTop [2] LeftBottom [3] RightBottom
    private int ktProgress;  // 0 == Need Dish Install  1 == Need Soup Install  2 == Need Soup & Poision Install 3 == Ending


    [Header("Script Variable")]
    [SerializeField] private KTDish[] ktDish = new KTDish[4];
    [SerializeField] private KTEvent ktEvent;

    protected override void LoadEachPuzzleData() {
        KitchenTableData saveData = PuzzleManager.instance.puzzleData.floor2Data.saveKitchenTable;

        ktDishOnTable = saveData.ktDishOnTable;
        ktSoupOnTable = saveData.ktSoupOnTable;
        ktPoisionOnTable = saveData.ktPoisionOnTable;
        ktProgress = saveData.ktProgress;
    }

    public override void SaveEachPuzzleData(PuzzleData a_puzzleSaveData) {
        a_puzzleSaveData.floor2Data.saveKitchenTable.ktDishOnTable = ktDishOnTable;
        a_puzzleSaveData.floor2Data.saveKitchenTable.ktSoupOnTable = ktSoupOnTable;
        a_puzzleSaveData.floor2Data.saveKitchenTable.ktPoisionOnTable = ktPoisionOnTable;
        a_puzzleSaveData.floor2Data.saveKitchenTable.ktProgress = ktProgress;
    }

    protected override void SetupPuz(){
        for(int i = 0; i < 4; i++){
            ktDish[i].Init(ktDishOnTable[i], ktSoupOnTable[i]);
        }
        CheckDishCondition();
    }
    
    public void SetDishTable(int dishPlaceNum, int dishNum){
        ktDishOnTable[dishPlaceNum] = dishNum;
    }

    public void SetSoupTable(int dishPlaceNum, int soupNum){
        ktSoupOnTable[dishPlaceNum] = soupNum;
    }

    public void SetPoisionTable(int dishPlaceNum, bool hasPoision){
        ktPoisionOnTable[dishPlaceNum] = hasPoision;
    }

    public int GetProgress(){
        return ktProgress;
    }

    public void CheckDishCondition(){
        switch(ktProgress){
            case 0:
                bool isCorrectDish = true;
                for(int i = 0; i < 4; i++){
                    if(ktDishOnTable[i] != ktCorrectDish[i]) isCorrectDish = false;
                }
                
                if(isCorrectDish) {
                    Progress0Event();
                }
                break;

            case 1:
                bool isCorrectSoup = true;
                for(int i = 0; i < 4; i++){
                    if(ktSoupOnTable[i] != ktCorrectSoup[i]) isCorrectSoup = false;
                }

                if(isCorrectSoup){
                    Progress1Event();
                }
                break;

            case 2:
                bool isCorrectPoision = true;
                for(int i = 2; i < 4; i++){
                    if(ktPoisionOnTable[i] != true) isCorrectPoision = false;
                }

                if(isCorrectPoision){
                    Progress2Event();
                }
                break;

            case 3:

                break;
            default:
                break;
        }
    }


    private void Progress0Event(){
        ktProgress = 1;
        Debug.Log("Talking Event: Mom I'm Hungry");
        ktEvent.Progress0Event();
    }

    private void Progress1Event(){
        ktProgress = 2;
        Debug.Log("Eating Food Event");
        // Fade In
        // Delete Lily Plate
        // Everyone eat soup
        // Fade Out
    }

    private void Progress2Event(){
        ktProgress = 3;
        Debug.Log("Talking Event: Mom Why I can't Eat");
        // Talking Event
        // Fade In OUT & MOM Poision SOUP Item generate
    }
}
