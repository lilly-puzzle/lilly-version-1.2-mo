using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MHEnum;

public class MHMouse : SimpleOneTouch
{
    [Header("Script Variable")]
    [SerializeField] private MouseHole mhManager;
    [SerializeField] private GameObject Note;
    
    public void Init(MHMouseCondition condition){
        switch(condition){
            case MHMouseCondition.InHole:
                Color color = this.GetComponent<SpriteRenderer>().color;
                color.a = 0.0f;
                this.GetComponent<SpriteRenderer>().color = color;
                break;
            case MHMouseCondition.Dead:
                
                break;
            default:
                break;
        }
    }


    // 쥐는 고양이 스프리아트에 포함 시키고 쪽지만 따로 넣는것 고려
    public void RevealMouse(){
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a = 1.0f;
        this.GetComponent<SpriteRenderer>().color = color;

        Note.SetActive(true); 
    }

    protected override void FuncWhenTouchEnded(){
        if(mhManager.mhMouseCondition == MHMouseCondition.DeadHasItem){
            InventoryManager.instance.PushItem(10302);

            mhManager.mhMouseCondition = MHMouseCondition.Dead;
        }
    }
}
