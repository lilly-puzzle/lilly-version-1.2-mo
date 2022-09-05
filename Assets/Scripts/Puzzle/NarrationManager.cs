using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class NarrationManager : MonoBehaviour
{
    public static NarrationManager instance;

    [SerializeField] private GameObject narrationObject;
    [SerializeField] private TextMeshProUGUI narrationMent;
    private Coroutine narrationCoroutine;

    public delegate void Callback();

    void Awake(){
        instance = this;
    }

    public void NarrationEnd(){
        narrationMent.text = "";
        narrationObject.SetActive(false);
    }

    public void Narration( string ment, float typingSpeed, Callback callbackFunction = null){
        if(narrationCoroutine != null){
            StopCoroutine(narrationCoroutine);
        }
        narrationCoroutine = StartCoroutine(NarrationCor( ment, typingSpeed, callbackFunction));
    }

    IEnumerator NarrationCor( string ment, float typingSpeed, Callback callbackFunction = null){
        narrationObject.SetActive(true);
        
        int index = 0;
        narrationMent.text = "";

        while(narrationMent.text != ment){
            narrationMent.text += ment[index];
            index++;
            yield return new WaitForSeconds( 1 / typingSpeed);
        }
        
        if(callbackFunction != null){
            callbackFunction();
        }
    }

    // Need Touch Block
}
