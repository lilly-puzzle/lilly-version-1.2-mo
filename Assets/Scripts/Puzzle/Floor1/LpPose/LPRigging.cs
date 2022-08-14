using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPRigging : SimpleOneTouch
{
    [Header("Object Variables")]
    [SerializeField]private GameObject[] bones;     /// [0] Left Shoulder, [1] Left Elbow,  [2] RS, [3] RE, [4] Left Hip Joint [5] Left Knee [6] Right Hip Joint [7] Right Knee
    [SerializeField]private GameObject[] sprites;

    [Header("Script Variables")]
    [SerializeField]private LPpose LPmanager;

    public void Init(){
        if(LPmanager.isDollExit){
            setVisible();
            for(int i = 0; i < LPpose.RiggingBonesNum; i++){
                setRiggingAngle(i, LPmanager.DollRigging[i]);
            }
        }
        else{
            setTransparent();
        }
    }

    public void setRiggingAngle(int RiggingNum, float angle){
        bones[RiggingNum].transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    protected override void FuncWhenTouchEnded(){
        if(InventoryManager.instance.curSelectedItem == 10505){    // Definitely this puzzle code 105
            setVisible();
            InventoryManager.instance.PopItem();
            LPmanager.isDollExit = true;
            LPmanager.checkChange(3);
        }
        else{
            setTransparent();
            InventoryManager.instance.PushItem(10505);
            LPmanager.isDollExit = false;
            LPmanager.checkChange(3);
        }
    }

    public void setVisible(){
        for (int i = 0; i < sprites.Length; i++){
            Color color = sprites[i].GetComponent<SpriteRenderer>().color;
            color.a = 1.0f;
            sprites[i].GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void setTransparent(){
        for (int i = 0; i < sprites.Length; i++){
            Color color = sprites[i].GetComponent<SpriteRenderer>().color;
            color.a = 0.0f;
            sprites[i].GetComponent<SpriteRenderer>().color = color;
        }
    }
}
