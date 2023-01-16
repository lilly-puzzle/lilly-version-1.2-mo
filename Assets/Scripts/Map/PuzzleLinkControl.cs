using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLinkControl : SimpleOneTouch
{
    public int PuzzleCode;

    protected override void FuncWhenTouchEnded() {
        if(!JoystickManager.instance.GetisJoystickAct()){
            PlayerPrefs.SetInt("Puzzle Code", PuzzleCode);
            TransitionManager.instance.SceneTransition("PuzzleScene");
        }
    }
}
