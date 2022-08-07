using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable] private class ItemSpriteByPuzzle {
        public List<Sprite> spriteList;
    }

    [System.Serializable] private class ItemSpritePerFloor {
        public List<ItemSpriteByPuzzle> spriteByPuzzle;
    }

    public static InventoryManager instance;

    [Header("Variables")]
    private bool isChanged = false;
    public int curSelectedItem { get; private set; }

    [Header("Sprite Variables")]
    [SerializeField] private List<ItemSpritePerFloor> itemSprite;

    [Header("Object Variables")]
    [SerializeField] private List<GameObject> inventoryObj;
    [SerializeField] private GameObject zoomObj;

    [Header("Script Variables")]
    private INZoomControl zoomScript;

    // setter
    public void SetIsChanged(bool a_isChanged) { isChanged = a_isChanged; }

    [Header("InventoryData Variables")]
    private List<int> playerInventory;

    private void Awake() {
        instance = this;

        zoomScript = zoomObj.GetComponent<INZoomControl>();
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

    // activate inventory
    public void ActivateInventory(bool a_needToActivate, int a_sceneNum) {
        if (a_needToActivate) {
            inventoryObj[a_sceneNum].SetActive(true);
        } else {
            foreach (GameObject inv in inventoryObj) {
                inv.SetActive(false);
            }
        }
    }

    // about item
    public void PushItem(int a_itemCode) {
        playerInventory.Add(a_itemCode);
    }

    public void PopItem(int a_itemCode) {
        // find idx, and if exists, pop
    }

    public void SelectItem(int a_itemCode) {
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
}
