using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor1;

using MHEnum;

public class MouseHole : PuzzleMainController
{
    
    [Header("Script Variables")]
    [SerializeField] private MHCat mhCat;
    [SerializeField] private MHHole mhHole;
    [SerializeField] private MHMouse mhMouse;
    [SerializeField] private MHPlate mhPlate;

    [Header("Condition Variables")]
    public MHCatCondition mhCatCondition;
    public MHMouseCondition mhMouseCondition;
    public MHPlateCondition mhPlateCondition;

    protected override void LoadEachPuzzleData() {
        MouseHoleData saveData = PuzzleManager.instance.puzzleData.floor1Data.saveMouseHole;

        mhCatCondition = (MHCatCondition)saveData.mhCatCondition;
        mhMouseCondition = (MHMouseCondition)saveData.mhMouseCondition;
        mhPlateCondition = (MHPlateCondition)saveData.mhPlateCondition;
    }

    public override void SaveEachPuzzleData(PuzzleData a_puzzleSaveData) {
        a_puzzleSaveData.floor1Data.saveMouseHole.mhCatCondition = (int)mhCatCondition;
        a_puzzleSaveData.floor1Data.saveMouseHole.mhMouseCondition = (int)mhMouseCondition;
        a_puzzleSaveData.floor1Data.saveMouseHole.mhPlateCondition = (int)mhPlateCondition;
    }
    
    protected override void SetupPuz(){
        mhCat.Init(mhCatCondition);
        mhHole.Init(mhMouseCondition);
        mhMouse.Init(mhMouseCondition);
        mhPlate.Init(mhPlateCondition);
    }

    public void FillPlate(){
        mhPlateCondition = MHPlateCondition.Full;
        mhCatCondition = MHCatCondition.Eating;
        mhCat.EatMilk();
    }

    public void GetMouse(){
        mhMouseCondition = MHMouseCondition.DeadHasItem;
        mhMouse.RevealMouse();
    }
}
