using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemFloor{
    public ItemPuzzle[] itemPuzzle = new ItemPuzzle[16]; 
}

[System.Serializable]
public class ItemPuzzle{
    public GameObject[] itemObject = new GameObject[10];
}

public class ItemLinkManager : MonoBehaviour
{
    public static ItemLinkManager instance;

    [Header("Object Variable")]
    [SerializeField] private ItemFloor[] itemFloor = new ItemFloor[5];

    
    [Header("Variable")]

    private int playerOnFloor;

    private int[,] itemStatus;

    void Awake(){
        instance = this;
        LoadItem();
    }

    private void LoadItem(){
        playerOnFloor = DataManager.gameData.characterData.characterPos.curFloor;

        itemStatus = DataManager.gameData.playData.activeObject;

        for(int _puzzle = 1; _puzzle < DefaultData.MAX_NUM_OF_PUZ_PER_FLOOR; _puzzle++){
            int itemStatusInPuzzle = itemStatus[playerOnFloor, _puzzle];

            for(int _item = 1; _item < DefaultData.MAX_NUM_OF_ITEMS_PER_PUZ; _item++){
                if (((itemStatusInPuzzle >> _item) & 1) == 0)
                {
                    int temp = playerOnFloor * 10000;
                    temp += _puzzle * 100;
                    temp += _item;

                    if (itemFloor[playerOnFloor].itemPuzzle[_puzzle].itemObject[_item] != null){
                        itemFloor[playerOnFloor].itemPuzzle[_puzzle].itemObject[_item].SetActive(false);
                    }
                }
            }
        }
    }

    public void deleteItem(int itemcode){
        int del_floor = itemcode / 10000;
        int del_puzzle = (itemcode % 10000) / 100;
        int del_item = (itemcode % 10000) % 100;
        Debug.Log("F" +itemStatus[del_floor, del_puzzle]);
        itemStatus[del_floor, del_puzzle] = itemStatus[del_floor, del_puzzle]^(1<<del_item);
        DataManager.instance.SetActiveObject(itemStatus);
        Debug.Log(itemStatus[del_floor, del_puzzle]);
    }
}
