using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataFrame;
using SaveDataPerPuzzle.Floor1;

public class PlainHanger : PuzzleMainController
{
    [System.Serializable] private class ClothesSprite {
        public List<Sprite> spriteList;
    }

    [System.Serializable] private class ClutterObject {
        public List<GameObject> objectList;
    }

    [System.Serializable] private class HangerControlScript {
        public List<PHHangerControl> scriptList;
    }

    public static PlainHanger instance;

    [Header("Constant Variables")]
    private const int NUM_OF_HANGER = 8;
    private const int NUM_OF_HANGER_TYPE = 2;
    private const int NUM_OF_HANGER_NUM = 4;
    private const int ANS_BIT_MASK = (1 << 8) - 1;

    [Header("Variables")]
    private int numOfCorrectClothes;
    private List<List<int>> hangingClothesCode;
    public bool isDragging { get; private set; }
    public bool isCleared { get; private set; }
    private bool hasPaper;

    [Header("Sprite Variables")]
    [SerializeField] private List<ClothesSprite> hangerWithClothes;
    [SerializeField] private List<ClothesSprite> unrolledClothes;
    [SerializeField] private Sprite dadBottomsWithoutPaper;

    [Header("Object Variables")]
    [SerializeField] private List<ClutterObject> clutterObject;
    [SerializeField] private GameObject draggingObject;

    [Header("Script Variables")]
    [SerializeField] private PHDraggingControl draggingScript;
    [SerializeField] private PHHangerControl dadBottomsHangerScript;
    [SerializeField] private PHPaperControl paperScript;

    private new void Awake() {
        base.Awake();
        instance = this;
    }

    public override void SaveEachPuzzleData(PuzzleData a_puzzleData) {
        PlainHangerData data = a_puzzleData.floor1Data.savePlainHanger;

        data.hangingClothesCode = hangingClothesCode;
        data.hasPaper = hasPaper;
    }

    protected override void LoadEachPuzzleData() {
        PlainHangerData data = PuzzleManager.instance.puzzleData.floor1Data.savePlainHanger;

        hangingClothesCode = data.hangingClothesCode;
        hasPaper = data.hasPaper;
    }

    protected override void SetupPuz() {
        numOfCorrectClothes = 0;
        isDragging = false;
        isCleared = false;

        Transform closet = transform.GetChild(1);
        for (int hangerType = 1; hangerType <= NUM_OF_HANGER_TYPE; hangerType++) {
            Transform hangerPerType = closet.GetChild(hangerType);

            for (int hangerNum = 0; hangerNum < NUM_OF_HANGER_NUM; hangerNum++) {
                int clothesCode = hangingClothesCode[hangerType][hangerNum];
                if (clothesCode == -1) continue;

                PHHangerControl hangerScript = hangerPerType.GetChild(hangerNum).gameObject.GetComponent<PHHangerControl>();
                int clothesNum = clothesCode % 10;
                bool result;

                result = hangerScript.HangingClothes(clothesCode, hangerWithClothes[hangerType].spriteList[clothesNum]);
                clutterObject[hangerType].objectList[clothesNum].SetActive(false);

                if (result) numOfCorrectClothes |= (1 << (hangerType - 1) * NUM_OF_HANGER_NUM + clothesNum);
            }
        }

        paperScript.SetHasPaper(hasPaper);

        CheckIfClear();
    }

    public void StartToDragClothes(int a_clothesCode) {
        isDragging = true;

        int clothesType = a_clothesCode / 10;
        int clothesNum = a_clothesCode % 10;

        draggingObject.SetActive(true);
        draggingScript.SetClothes(a_clothesCode, unrolledClothes[clothesType].spriteList[clothesNum]);
    }

    public void FailToDragClothes(int a_clothesCode) {
        isDragging = false;

        int clothesType = a_clothesCode / 10;
        int clothesNum = a_clothesCode % 10;

        clutterObject[clothesType].objectList[clothesNum].SetActive(true);
    }

    public void TryToHangClothes(GameObject a_hangerObject, int a_clothesCode) {
        isDragging = false;

        PHHangerControl hanger = a_hangerObject.GetComponent<PHHangerControl>();

        if (!hanger.CheckCanHang(a_clothesCode)) {
            FailToDragClothes(a_clothesCode);
            return;
        }

        int clothesType = a_clothesCode / 10;
        int clothesNum = a_clothesCode % 10;
        int shiftIdx = (clothesType - 1) * NUM_OF_HANGER_NUM + clothesNum;

        bool result;
        result = hanger.HangingClothes(a_clothesCode, hangerWithClothes[clothesType].spriteList[clothesNum]);

        numOfCorrectClothes &= ANS_BIT_MASK - (1 << shiftIdx);
        if (result) numOfCorrectClothes |= (1 << shiftIdx);

        CheckIfClear();

        int hangerCode = hanger.GetHangerCode();
        int hangerType = hangerCode / 10;
        int hangerNum = hangerCode % 10;

        hangingClothesCode[hangerType][hangerNum] = a_clothesCode;
    }

    public void RemoveClothesFromHanger(int a_hangerCode, int a_clothesCode) {
        SpecificClothesToClutter(a_clothesCode);

        int hangerType = a_hangerCode / 10;
        int hangerNum = a_hangerCode % 10;

        hangingClothesCode[hangerType][hangerNum] = -1;
    }

    public void SpecificClothesToClutter(int a_clothesCode) {
        int clothesType = a_clothesCode / 10;
        int clothesNum = a_clothesCode % 10;

        clutterObject[clothesType].objectList[clothesNum].SetActive(true);
    }

    private void CheckIfClear() {
        if (numOfCorrectClothes == ANS_BIT_MASK) {
            isCleared = true;
            PuzzleClear();
        }
    }

    public void GetPaperFromDadBottoms() {
        dadBottomsHangerScript.SetSprite(dadBottomsWithoutPaper);

        hasPaper = false;
    }
}
