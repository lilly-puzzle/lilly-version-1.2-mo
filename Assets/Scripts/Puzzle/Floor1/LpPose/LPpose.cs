using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor1;

public class LPpose : PuzzleMainController
{
    [Header("Constant Variables")]
    private readonly int[,] LPRiggingConnect = new int[5, 2]{
        {-1, -1},   // LP 0 is None
        {0, 3},     // LP 1 connects [0] Left Shoulder [3] Right Elbow
        {1, 2},     // LP 2 connects [1] LE  [2] RS
        {4, 7},     // LP 3 connects [4] Left Hip Joint [7] Right Knee
        {5, 6},     // LP 4 connects [5] Left Knee [6] Right Hip Joint
    };
    public const int RiggingBonesNum = 8;
    private readonly float[,] DollRiggingRange = new float[8,2] {
        {70, 145},
        {270, 380},
        {70, 140},
        {280, 380},
        {105, 200},
        {-20, 105},
        {105, 200},
        {-20, 90},
    };
    private readonly float[] RiggingSpeed = new float[5] { 0.0f, 35f, 45f, 55f, 65f};   // [0] is None ,[1~4] means LP Record
    private const float RiggingAccelTime = 0.5f;
    private readonly float[] CorrectRigging = new float[8] { 90.0f, 360.0f, 270.0f, 360.0f, 180.0f, 0.0f, 180.0f, 0.0f};
    private const float CorrectDeviation = 10.0f;

    [Header("Variables")]
    public int installedLP =1;     // 0 is None, 1 ~ 4 LP
    //private float[] DollRigging;    // [0] Left Shoulder, [1] Left Elbow,  [2] RS, [3] RE, [4] Left Hip Joint [5] Left Knee [6] Right Hip Joint [7] Right Knee
    //Test
    public float[] DollRigging = new float[] {145.0f, 270.0f, 140.0f, 280.0f, 105.0f, 105.0f, 105.0f, 90.0f};
    private int[] DollRiggingDir = new int[] {1, 1, 1, 1, 1, 1, 1, 1};   // 1 == + -1 == -
    private float[] LPSoundProgress; // [0] is None, [1] ~ [4] LP   Sound Progress Save ex) 0:00 Start 0:20 Stop -> 0:20 Start
    private Coroutine SoundCoroutine;
    private Coroutine RiggingCoroutine1;
    private Coroutine RiggingCoroutine2;
    private bool isRiggingPlay = false;
    public bool isCorrect = false;
    public bool isDollExit = true;
    public bool isNeedleDown = false;

    [Header("Script Variables")]
    [SerializeField] private LPRigging LPRigging_;
    [SerializeField] private LPNeedle LPNeedle_;
    [SerializeField] private LPTable LPTable_;
    
    public override void SaveEachPuzzleData(PuzzleData a_puzzleSaveData) {
        checkCorrect();
        a_puzzleSaveData.floor1Data.saveLPpose.installedLP = installedLP;
        a_puzzleSaveData.floor1Data.saveLPpose.DollRigging = DollRigging;
        a_puzzleSaveData.floor1Data.saveLPpose.DollRiggingDir = DollRiggingDir;
        a_puzzleSaveData.floor1Data.saveLPpose.LPSoundProgress = LPSoundProgress;
        a_puzzleSaveData.floor1Data.saveLPpose.isCorrect = isCorrect;
        a_puzzleSaveData.floor1Data.saveLPpose.isDollExit = isDollExit;
        a_puzzleSaveData.floor1Data.saveLPpose.isNeedleDown = isNeedleDown;
    }

    protected override void LoadEachPuzzleData() {
        LPposeData saveData = PuzzleManager.instance.puzzleData.floor1Data.saveLPpose;

        installedLP = saveData.installedLP;
        DollRigging = saveData.DollRigging;
        DollRiggingDir = saveData.DollRiggingDir;
        LPSoundProgress = saveData.LPSoundProgress;
        isCorrect = saveData.isCorrect;
        isDollExit = saveData.isDollExit;
        isNeedleDown = saveData.isNeedleDown;
    }

    protected override void SetupPuz(){
        LPRigging_.Init();
        LPNeedle_.Init();
        LPTable_.Init();
    }

    public void checkChange(int casenum){   // 1 == LPTable.cs  2 == LPNeedle.cs   3 == LPRigging.cs
        switch(casenum){
            case 1:         // LPTable Touch!       1. LP Record Animation     2. playLPRecord  3. stopLPRecord
                if(isDollExit){
                    LPTable_.startAnimation();
                    if(isNeedleDown){
                        playLPRecord();
                    }
                }
                break;
            case 2:         // LPNeedle Touch!      1. playLPRecord
                if(isNeedleDown){
                    if(isDollExit){
                        playLPRecord();
                    }
                }
                else{
                    stopLPRecord();
                }
                break;
            case 3:         // LPRigging(Doll) Touch! 1. LP Record Animation    2. playLPRecord    3.stopLPRecord
                if(isDollExit){
                    LPTable_.startAnimation();
                    if(isNeedleDown){
                        playLPRecord();
                    }
                }
                else{
                    LPTable_.stopAnimation();
                    stopLPRecord();
                }
                break;
        }
    }

    public void playLPRecord(){
        if (installedLP != 0){
            // Need Sound: LPRecord music play (fadein -> play)
            if (RiggingCoroutine1 != null){
                StopCoroutine(RiggingCoroutine1);
            }
            if (RiggingCoroutine2 != null){
                StopCoroutine(RiggingCoroutine2);
            }
            RiggingCoroutine1 = StartCoroutine(playRigging(LPRiggingConnect[installedLP, 0]));
            RiggingCoroutine2 = StartCoroutine(playRigging(LPRiggingConnect[installedLP, 1]));
        }
        else{   // installedLP == 0 is None
            stopLPRecord();
        }
    }

    IEnumerator playRigging(int RiggingNum){
        isRiggingPlay = true;
        float steptimer = 0.0f;
        float accel = 0.0f;
        float rate;
        while(isRiggingPlay){
            rate = steptimer / RiggingAccelTime;
            accel = Mathf.Min(rate, 1.0f);
            if(DollRigging[RiggingNum] <= DollRiggingRange[RiggingNum, 0]){
                DollRiggingDir[RiggingNum] = 1;
            }
            else if(DollRigging[RiggingNum] >= DollRiggingRange[RiggingNum, 1]){
                DollRiggingDir[RiggingNum] = -1;
            }
            DollRigging[RiggingNum] += DollRiggingDir[RiggingNum] * accel * RiggingSpeed[installedLP] * Time.deltaTime;
            LPRigging_.setRiggingAngle(RiggingNum, DollRigging[RiggingNum]);
            yield return null;
            steptimer += Time.deltaTime;
        }
    }

    public void stopLPRecord(){
        // Need Sound: LPRecord music stop (play -> fadeout)
        isRiggingPlay = false;
    }

    private void checkCorrect(){
        bool isCorrect_ = true;
        for(int i = 0; i < RiggingBonesNum; i++){
            if(DollRigging[i] <= CorrectRigging[i] - CorrectDeviation || DollRigging[i] >= CorrectRigging[i] + CorrectDeviation){
                isCorrect_ = false;
            }
        }
        isCorrect = isCorrect_;
    }

    private void playMusic(){

    }
}
