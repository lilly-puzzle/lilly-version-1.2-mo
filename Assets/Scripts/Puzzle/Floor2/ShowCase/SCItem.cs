using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCItem : SimpleOneTouch
{
    [SerializeField] private ShowCase scManager;
    protected override void FuncWhenTouchEnded(){
        InventoryManager.instance.PushItem(20801);
        scManager.SetItemAct(false);
        this.gameObject.SetActive(false);
    }
}
