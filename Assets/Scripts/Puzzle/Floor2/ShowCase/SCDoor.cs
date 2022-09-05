using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCDoor : SimpleOneTouch
{
    [SerializeField] private bool isLeft;
    [SerializeField] private Sprite[] doorSprite = new Sprite[2];   // [0] == Open  [1] == Close
    [SerializeField] private ShowCase scManager;

    public void Open(){
        this.GetComponent<SpriteRenderer>().sprite = doorSprite[0];
    }

    public void Close(){
        this.GetComponent<SpriteRenderer>().sprite = doorSprite[1];
    }

    protected override void FuncWhenTouchEnded(){
        if(scManager.GetDoorOpen(isLeft)){
            Close();
            scManager.SetDoorOpen(isLeft, false);
        }
        else{
            Open();
            scManager.SetDoorOpen(isLeft, true);
        }
    }
}
