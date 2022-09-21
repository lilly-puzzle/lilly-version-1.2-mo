using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPTable : SimpleOneTouch
{
    [Header("Variables")]
    private int selectedItem_;

    [Header("Object Variables")]
    [SerializeField] private Sprite[] TableSprites = new Sprite[5];     // [0] is Transparent
    
    [Header("Script Variables")]
    [SerializeField] private LPpose LPmanager;

    public void Init(){
        setSprite();
    }

    protected override void FuncWhenTouchEnded(){
        selectedItem_ = InventoryManager.instance.curSelectedItem;
        if(LPmanager.installedLP == 0){    // LPtable doesn't have LP Record
            if(selectedItem_ >= 10501 && selectedItem_ <= 10504){       // Definitely this puzzle code 105
                LPmanager.installedLP = selectedItem_ % 10;
                setSprite();
                InventoryManager.instance.PopItem();
                LPmanager.checkChange(1);
            }
        }
        else{   // LP Table has LP Record 1, 2, 3, 4
            int LPitemcode = 10500 + LPmanager.installedLP;
            if(selectedItem_ >= 10501 && selectedItem_ <= 10504){
                InventoryManager.instance.PushItem(LPitemcode);
                LPmanager.installedLP = selectedItem_ % 10;
                setSprite();
                InventoryManager.instance.PopItem();
                LPmanager.checkChange(1);
            }
            else{
                InventoryManager.instance.PushItem(LPitemcode);
                LPmanager.installedLP = 0;
                setSprite();
                LPmanager.checkChange(1);
            }
        }
    }

    private void setSprite(){
        SpriteRenderer spriteRenderer_ = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer_.sprite =TableSprites[LPmanager.installedLP];
    }

    public void startAnimation(){

    }

    public void stopAnimation(){
        
    }
}
