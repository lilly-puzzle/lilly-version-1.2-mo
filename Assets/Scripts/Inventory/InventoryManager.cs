using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable] private class ItemSpriteByPuzzle {
        public List<Sprite> spriteList;
    }

    [System.Serializable] private class ItemSpritePerFloor {
        public List<ItemSpriteByPuzzle> spriteByPuzzle;
    }

    [System.Serializable] private class ItemScriptPerScene {
        public List<INItemControl> itemScript;
    }

    public static InventoryManager instance;

    [Header("Variables")]
    private bool isChanged = false;
    public int curSelectedItem { get; private set; }
    private int sceneNum;
    private PriorityQueue<int> popedItemIdx = new PriorityQueue<int>();
    public int[] slotStartIdx { get; private set; }
    // setter
    public void SetIsChanged(bool a_isChanged) { isChanged = a_isChanged; }
    public void SetSlotStartIdx(int a_startIdx) { slotStartIdx[sceneNum] = a_startIdx; }

    [Header("Sprite Variables")]
    [SerializeField] private List<ItemSpritePerFloor> itemSprite;

    [Header("Object Variables")]
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private GameObject zoomObj;

    [Header("UI Variables")]
    [SerializeField] private List<Button> leftBtn;
    [SerializeField] private List<Button> rightBtn;

    [Header("Script Variables")]
    private INZoomControl zoomScript;
    [SerializeField] private List<ItemScriptPerScene> itemScriptPerScene;

    [Header("InventoryData Variables")]
    private List<int> playerInventory;

    private void Awake() {
        instance = this;

        zoomScript = zoomObj.GetComponent<INZoomControl>();

        slotStartIdx = new int[2] { 0, 0 };

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        LoadInventoryData();
    }

    // save & load
    public void SaveInventoryData() {
        if (isChanged) {
            DataManager.instance.SetInventoryData(playerInventory);
        }
    }

    private void LoadInventoryData() {
        playerInventory = DataManager.gameData.characterData.playerInventory;
    }

    // activate inventory when scene change
    public void ActivateInventory(bool a_needToActivate, int a_sceneNum) {
        if (a_needToActivate) {
            canvasObj.SetActive(true);
            sceneNum = a_sceneNum - DefaultData.MAP_SCENE_IDX_NUM;

            UpdateSlot();
        } else {
            canvasObj.SetActive(false);
        }
    }

    // about slot
    public bool PushItem(int a_itemCode) {
        int expectedSlotIdx;

        if (popedItemIdx.Count != 0) {
            int firstPopIdx = popedItemIdx.Pop();
            playerInventory[firstPopIdx] = a_itemCode;

            expectedSlotIdx = firstPopIdx - slotStartIdx[sceneNum];
        } else if (DefaultData.SIZE_OF_INVENTORY <= playerInventory.Count) {
            return false;
        } else {
            playerInventory.Add(a_itemCode);

            expectedSlotIdx = playerInventory.Count - slotStartIdx[sceneNum] - 1;
        }

        if (IsInSlotRange(expectedSlotIdx, DefaultData.NUM_OF_INVENTORY_SLOT[sceneNum])) {
            ShowItem(expectedSlotIdx, a_itemCode);
        }

        return true;
    }

    public void PopItem() {
        if (curSelectedItem == -1) return;

        int idxOfItem = playerInventory.IndexOf(curSelectedItem);

        curSelectedItem = -1;

        playerInventory[idxOfItem] = -1;
        popedItemIdx.Add(idxOfItem);

        int expectedSlotIdx = idxOfItem - slotStartIdx[sceneNum];
        ShowItem(expectedSlotIdx, -1);
    }

    public void SelectItem(int a_itemCode) {
        if (a_itemCode == -1) { curSelectedItem = -1; return; }
        
        int floorNum = a_itemCode / 10000;
        int puzzleNum = a_itemCode % 10000 / 100;
        int itemNum = a_itemCode % 10000 % 100;

        if (((DefaultData.ZOOM_OR_NOT[floorNum][puzzleNum] >> itemNum) & 1) == 1) {
            zoomObj.SetActive(true);
            zoomScript.ZoomItem(itemSprite[floorNum].spriteByPuzzle[puzzleNum].spriteList[itemNum]);

            curSelectedItem = -1;
        } else {
            curSelectedItem = (curSelectedItem != a_itemCode) ? a_itemCode : -1;
        }
    }

    private void ShowItem(int a_slotIdx, int a_itemCode) {
        if (a_itemCode == -1) {
            itemScriptPerScene[sceneNum].itemScript[a_slotIdx].SetItem(-1, null);
            return;
        }

        int floorNum = a_itemCode / 10000;
        int puzzleNum = a_itemCode % 10000 / 100;
        int itemNum = a_itemCode % 10000 % 100;
        
        itemScriptPerScene[sceneNum].itemScript[a_slotIdx].SetItem(a_itemCode, itemSprite[floorNum].spriteByPuzzle[puzzleNum].spriteList[itemNum]);
    }

    public void UpdateSlot() {
        int startIdx = slotStartIdx[sceneNum];
        int numOfItemInInventory = playerInventory.Count;

        for (int i = 0; i < DefaultData.NUM_OF_INVENTORY_SLOT[sceneNum]; i++) {
            int slotIdx = startIdx + i;
            ShowItem(i, (slotIdx < numOfItemInInventory) ? playerInventory[slotIdx] : -1);
        }

        if (startIdx == 0) leftBtn[sceneNum].interactable = false;
        else leftBtn[sceneNum].interactable = true;

        if (startIdx == DefaultData.SIZE_OF_INVENTORY - DefaultData.NUM_OF_INVENTORY_SLOT[sceneNum]) rightBtn[sceneNum].interactable = false;
        else rightBtn[sceneNum].interactable = true;
    }

    // function to check
    private bool IsInSlotRange(int a_expectedIdx, int a_numOfSlot) {
        if (0 <= a_expectedIdx && a_expectedIdx < a_numOfSlot) return true;
        else return false;
    }
}
