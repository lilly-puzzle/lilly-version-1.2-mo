using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Variables")]
    private bool isChanged = false;

    // setter
    public void SetIsChanged(bool a_isChanged) { isChanged = a_isChanged; }

    [Header("InventoryData Variables")]
    private List<int> playerInventory;

    private void Awake() {
        instance = this;
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

    // about item
    public void PushItem(int a_itemCode) {
        playerInventory.Add(a_itemCode);
    }

    public void PopItem(int a_itemCode) {
        // find idx, and if exists, pop
    }

    public void SelectItem(int a_itemCode) {
        // make zoom item list for map structure
        // if item belongs to list, zoom item
        // else set item code to select variable
    }
}
