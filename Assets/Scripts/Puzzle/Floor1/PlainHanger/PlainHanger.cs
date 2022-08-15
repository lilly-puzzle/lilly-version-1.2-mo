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

    [Header("Variables")]
    private int numOfCorrectClothes;
    private List<List<int>> hangingClothesCode;
    public bool isDragging { get; private set; }

    [Header("Sprite Variables")]
    [SerializeField] private List<ClothesSprite> hangerWithClothes;
    [SerializeField] private List<ClothesSprite> unrolledClothes;

    [Header("Object Variables")]
    [SerializeField] private List<ClutterObject> clutterObject;
    [SerializeField] private GameObject draggingObject;

    [Header("Script Variables")]
    [SerializeField] private PHDraggingControl draggingScript;
    [SerializeField] private List<HangerControlScript> hangerScript;

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
}
