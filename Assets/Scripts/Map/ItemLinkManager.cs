using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectFloor{
    public GameObject[] itemObjects;
}

public class ItemLinkManager : MonoBehaviour
{
    public static ItemLinkManager instance;

    [Header("Object Variable")]
    [SerializeField] private ObjectFloor[] linkFloor;
    private ItemLinkControl[,] links = new ItemLinkControl[5, 32];

    
    [Header("Variable")]
    private List<int> ActItems = new List<int>();

    private int playerOnFloor;

    private int[,] itemStatus;

    void Awake(){
        instance = this;
        LoadItem();
        SetUpItem();
    }

    private void LoadItem(){
        playerOnFloor = DataManager.gameData.characterData.characterPos.curFloor;

        itemStatus = DataManager.gameData.playData.activeObject;

        for(int _puzzle = 1; _puzzle < 16; _puzzle++){
            int itemStatusInPuzzle = itemStatus[playerOnFloor, _puzzle];

            for(int _item = 1; _item < 10; _item++){
                if (((itemStatusInPuzzle >> _item) & 1) == 1)
                {
                    int temp = playerOnFloor * 10000;
                    temp += _puzzle * 100;
                    temp += _item;

                    ActItems.Add(temp);
                }
            }
        }
    }

    private void SetUpItem(){
        for(int linkNum = 0; linkNum < linkFloor[playerOnFloor].itemObjects.GetLength(0); linkNum++){
            links[playerOnFloor, linkNum] = linkFloor[playerOnFloor].itemObjects[linkNum].GetComponent<ItemLinkControl>();
            for(int i = 0; i < ActItems.Count; i++){
                if(links[playerOnFloor, linkNum].ItemCode == ActItems[i]){
                    linkFloor[playerOnFloor].itemObjects[linkNum].SetActive(true);
                }
                else{
                    linkFloor[playerOnFloor].itemObjects[linkNum].SetActive(false);
                }
            }
        }
    }

    public void deleteItem(int itemcode){
        int del_floor = itemcode / 10000;
        int del_puzzle = (itemcode % 10000) / 100;
        itemStatus[del_floor, del_puzzle] = itemStatus[del_floor, del_puzzle]^(1<<itemcode);
    }
}
