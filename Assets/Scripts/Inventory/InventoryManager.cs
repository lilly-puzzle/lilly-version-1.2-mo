using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Variables")]
    private bool isChanged = false;

    // setter
    public void SetIsChanged(bool a_isChanged) { isChanged = a_isChanged; }

    [Header("InventoryData Variables")]
    private List<int> playerInventory;

    private void Start() {
        LoadInventoryData();
    }

    public void SaveInventoryData() {
        if (isChanged) {
            DataManager.instance.SetInventoryData(playerInventory);
        }
    }

    private void LoadInventoryData() {
        playerInventory = DataManager.gameData.characterData.playerInventory;
    }
}
