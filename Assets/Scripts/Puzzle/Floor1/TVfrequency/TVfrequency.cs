using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor1;

public class TVfrequency : PuzzleMainController
{
    public static TVfrequency instance;

    [Header("Constant Variables")]
    private const int NUM_OF_FREQUENCY = 3;
    private const int NUM_OF_SWITCH_LOC = 3;
    private const int NUM_OF_DIAL_ANGLE = 8;
    private readonly List<int> SWITCH_VALUE_LIST = new List<int> { 1, 3, 5 };
    private readonly List<int> DIAL_VALUE_LIST = new List<int> { 1, 3, 5, 6, 7, 9, 11, 12 };
    private readonly List<int> FREQUENCY_LIST = new List<int> { 60, 18, 5, 0 };

    [Header("Variables")]
    private int numOfTunedFrequencies;

    [Header("Script Variables")]
    [SerializeField] private TVProgressControl progressScript;
    [SerializeField] private List<TVLedControl> ledScriptList;
    [SerializeField] private TVSwitchDrag switchScript;
    [SerializeField] private TVDialDrag dialScript;

    private new void Awake() {
        base.Awake();
        instance = this;
    }

    public override void SaveEachPuzzleData(PuzzleData a_puzzleData) {
        TVfrequencyData data = a_puzzleData.floor1Data.saveTVfrequency;

        data.numOfTunedFrequencies = numOfTunedFrequencies;
        data.curSwitchIdx = switchScript.switchIdx;
        data.curDialIdx = dialScript.dialIdx;
    }

    protected override void LoadEachPuzzleData() {
        TVfrequencyData data = PuzzleManager.instance.puzzleData.floor1Data.saveTVfrequency;

        numOfTunedFrequencies = data.numOfTunedFrequencies;
        switchScript.SetIdx(data.curSwitchIdx);
        dialScript.SetIdx(data.curDialIdx);
    }

    protected override void SetupPuz() {
        progressScript.MoveToPosition(numOfTunedFrequencies + 1);

        for (int i = numOfTunedFrequencies; i < NUM_OF_FREQUENCY; i++) {
            ledScriptList[i].SetColor(false);
        }

        switchScript.SetupPuz();
        dialScript.SetupPuz();
    }

    private void CheckSwitchDialValue() {
        if (SWITCH_VALUE_LIST[switchScript.switchIdx] * DIAL_VALUE_LIST[dialScript.dialIdx] == FREQUENCY_LIST[numOfTunedFrequencies]) {
            ledScriptList[numOfTunedFrequencies].SetColor(true);
            numOfTunedFrequencies++;

            progressScript.MoveToPosition(numOfTunedFrequencies + 1);

            if (numOfTunedFrequencies == NUM_OF_FREQUENCY) {
                PuzzleClear();
            }
        }
    }
}
