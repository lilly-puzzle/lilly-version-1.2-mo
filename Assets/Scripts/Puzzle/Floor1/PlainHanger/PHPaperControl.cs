using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHPaperControl : SimpleOneTouch
{
    [Header("Variables")]
    private bool hasPaper;
    // setter
    public void SetHasPaper(bool a_hasPaper) { hasPaper = a_hasPaper; }

    protected override void FuncWhenTouchEnded() {
        if (!PlainHanger.instance.isCleared) return;
        if (!hasPaper) return;
        
        InventoryManager.instance.PushItem(10100);
        hasPaper = false;

        PlainHanger.instance.GetPaperFromDadBottoms();
    }
}
