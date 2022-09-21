using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MHPlateCondition{
    Empty,
    Full,
}

public class MHPlate : SimpleOneTouch
{
    [Header("Script Variable")]
    [SerializeField] private MouseHole mhManager;

    [SerializeField] private Sprite[] plateSprites = new Sprite[2];     // [0] Empty Plate  [1] Full Plate
    public void Init(MHPlateCondition condition){
        switch(condition){
            case MHPlateCondition.Empty:
                this.GetComponent<SpriteRenderer>().sprite = plateSprites[0];
                break;
            case MHPlateCondition.Full:
                this.GetComponent<SpriteRenderer>().sprite = plateSprites[1];
                break;
            default:
                break;
        }
    }

    protected override void FuncWhenTouchEnded(){
        if(InventoryManager.instance.curSelectedItem == 10301){
            mhManager.FillPlate();
        }
    }
}
