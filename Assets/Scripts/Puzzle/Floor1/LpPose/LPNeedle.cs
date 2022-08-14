using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPNeedle : SimpleOneTouch
{
    [Header("Script Variables")]
    [SerializeField] private LPpose LPmanager;
    
    public void Init(){
        if(LPmanager.isNeedleDown){
            // Set NeedleDown Sprite
        }
        else{
            // Set NeedleUp Sprite
        }
        
    }

    protected override void FuncWhenTouchEnded(){
        if (!LPmanager.isNeedleDown){     // isNeedleDown == false
            // Need Needle DownAnimation
            LPmanager.isNeedleDown = true;
            LPmanager.checkChange(2);
        }
        else{
            // Need Needle Up Animation
            LPmanager.isNeedleDown = false;
            LPmanager.checkChange(2);
        }
        
    }
}
