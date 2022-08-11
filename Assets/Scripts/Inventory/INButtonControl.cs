using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class INButtonControl : MonoBehaviour
{
    [Header("Variables")]
    private bool isInvEnable;
    private int curSceneNum;

    [Header("Script Variables")]
    [SerializeField] private INMoveControl inventoryMove;

    private void Awake() {
        // curSceneNum = SceneManager.GetActiveScene().buildIndex - DefaultData.MAP_SCENE_IDX_NUM;
        curSceneNum = 2 - DefaultData.MAP_SCENE_IDX_NUM;
    }

    public void ToggleInventory() {
        isInvEnable = !isInvEnable;
        
        inventoryMove.ToggleInventoryPosition(isInvEnable);
    }

    public void ShiftSlot(int a_shiftDir) {
        if (a_shiftDir == 0) return;

        int startIdx = InventoryManager.instance.slotStartIdx[curSceneNum];
        int expectedSlotIdx = startIdx + DefaultData.NUM_OF_INVENTORY_SLOT[curSceneNum] * a_shiftDir;

        if (expectedSlotIdx < 0 || DefaultData.SIZE_OF_INVENTORY <= expectedSlotIdx) return;

        InventoryManager.instance.SetSlotStartIdx(expectedSlotIdx);
        InventoryManager.instance.UpdateSlot();
    }
}
