using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KTDish : SimpleOneTouch
{
    [Header("Variable")]
    [SerializeField] private int dishPlaceNum;
    private int dishNum;
    private int soupNum;
    private bool hasPoision;

    [Header("Script Variable")]
    [SerializeField] private KitchenTable ktManager;
    
    [Header("Object Variable")]
    [SerializeField] private Sprite[] dishSprites = new Sprite[5]; // [0] == None [1] == MomDish [2] == DadDish [3] == CecillyDish [4] == LilyDish
    [SerializeField] private GameObject soupObject;
    [SerializeField] private Sprite[] soupSprites = new Sprite[6]; // [0] == None [1~5]

    public void Init(int dishNum_, int soupNum_){
        SetDish(dishNum_);
        SetSoup(soupNum_);
    }

    public void SetDish(int dishNum_){
        dishNum = dishNum_;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = dishSprites[dishNum_];
        ktManager.SetDishTable(dishPlaceNum, dishNum);
        ktManager.CheckDishCondition();
    }

    public void SetSoup(int soupNum_){
        soupNum = soupNum_;
        soupObject.GetComponent<SpriteRenderer>().sprite = soupSprites[soupNum_];
        ktManager.SetSoupTable(dishPlaceNum, soupNum);
        ktManager.CheckDishCondition();
    }

    public void SetPoision(bool hasPoision_){
        hasPoision = hasPoision_;
        // 독 스프 변화가 필요한지 고민
        ktManager.SetPoisionTable(dishPlaceNum, hasPoision);
    }

    protected override void FuncWhenTouchEnded(){
        int itemCode = InventoryManager.instance.curSelectedItem;
        if(itemCode >= 20901 && itemCode <= 20904){     // Dish item 20901 ~ 20904
            if(ktManager.GetProgress() == 0){
                if (dishNum == 0) {   // 접시가 설치되지 않았을 때
                    InventoryManager.instance.PopItem();    // 인벤토리의 아이템을 제거 한 뒤
                    SetDish(itemCode % 20900);     // 접시를 설치한다.
                }
                else { // 이미 다른 dish가 설치되어 있을 때
                    InventoryManager.instance.PopItem();    // 인벤토리의 아이템을 제거 한 뒤
                    InventoryManager.instance.PushItem(dishNum + 20900);    // 인벤토리에 현재 접시를 추가하여 접시들을 교체하고
                    SetDish(itemCode % 20900);     // 접시를 설치한다.
                }
            }
        }
        else if(itemCode == 0){     // 맨손일때
            if (ktManager.GetProgress() == 0) {
                InventoryManager.instance.PushItem(dishNum + 20900);
                SetDish(0);
                SetSoup(0);
            }
        }
        else if(itemCode == 20905){      // Inventory에 Soup를 가지고 있을 때
            if (ktManager.GetProgress() == 1 && ktManager.GetProgress() == 2){
                if(dishNum != 0){
                    if (soupNum == 5){
                        SetSoup(1);
                    }
                    else {
                        SetSoup(soupNum + 1);
                    }
                }
            }
        }
        else if(itemCode == 20906){    // Inventory에 독약을 가지고 있을 때 
            // Todo: 독약의 itemCode를 정확히 파악하기
            if(dishNum == 1){   // Cecilly 접시의 경우 독 넣으면 엄마가 싫어하는 멘트
                // Talking Event
                Debug.Log("Talking Event: Mom love you");
            }
            else{
                SetPoision(true);
            }
        }

    }
}
