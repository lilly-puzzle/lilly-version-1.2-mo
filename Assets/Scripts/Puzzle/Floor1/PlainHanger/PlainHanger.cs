using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<HangerControlScript> hangerScript;
    [SerializeField] private PHPaperControl paperScript;

    private new void Awake() {
        base.Awake();
        instance = this;
    }

    protected override void SetupPuz() {
        isDragging = false;

        for (int hangerType = 1; hangerType <= NUM_OF_HANGER_TYPE; hangerType++) {
            for (int hangerNum = 0; hangerNum < NUM_OF_HANGER_NUM; hangerNum++) {
                int clothesCode = hangingClothesCode[hangerType][hangerNum];
                if (clothesCode == -1) continue;

                int clothesNum = clothesCode % 10;
                bool result = hangerScript[hangerType].scriptList[hangerNum].HangingClothes(clothesCode, hangerWithClothes[hangerType].spriteList[clothesNum]);
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
    }

    private void CheckIfClear() {
        if (numOfCorrectClothes == ANS_BIT_MASK) {
            isCleared = true;
            PuzzleClear();
        }
    }

    public void GetPaperFromDadBottoms() {
        const int bottomsHangerType = 1;
        const int dadHangerNum = 0;

        hangerScript[bottomsHangerType].scriptList[dadHangerNum].SetSprite(dadBottomsWithoutPaper);

        hasPaper = false;
    }
}
