using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLinkControl : MonoBehaviour
{
    public int PuzzleCode;

    void OnMouseUp(){
        if(!JoystickManager.instance.GetisJoystickAct()){
            PlayerPrefs.SetInt("Puzzle Code", PuzzleCode);
            TransitionManager.instance.SceneTransition("PuzzleScene");
        }
    }
}
