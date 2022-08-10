using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLinkControl : MonoBehaviour
{
    public int PuzzleCode;
    private void FuncWhenTouched(){
        PlayerPrefs.SetInt("PuzzleCode", PuzzleCode);
        TransitionManager.instance.SceneTransition("PuzzleScene");
    }
}
