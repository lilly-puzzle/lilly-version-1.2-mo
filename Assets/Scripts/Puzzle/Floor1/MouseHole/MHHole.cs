using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MHEnum;

public class MHHole : MonoBehaviour
{
    [Header("Script Variable")]
    [SerializeField] private MouseHole mhManager;
    
    public void Init(MHMouseCondition condition){
        switch(condition){
            case MHMouseCondition.InHole:
                // Sound(Mouse running away)
                Debug.Log("Sound(Mouse running away)");
                break;
            default:
                break;
        }
    }
}
