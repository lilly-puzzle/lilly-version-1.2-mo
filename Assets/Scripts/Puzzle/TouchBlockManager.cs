using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBlockManager : MonoBehaviour
{
    public static TouchBlockManager instance;
    [SerializeField] private GameObject BlockObject;
    private GameObject touchAbleUI;
    private Vector3 savePos;


    void Awake(){
        instance = this;
        BlockObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -5);
    }

    public void TouchBlock(GameObject touchAbleUI_ = null){
        BlockObject.SetActive(true);
        if(touchAbleUI_ != null){
            touchAbleUI = touchAbleUI_;
            savePos = touchAbleUI.GetComponent<RectTransform>().anchoredPosition;
            touchAbleUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(savePos.x, savePos.y, -6);
        }
    }

    public void TouchBlockEnd(){
        BlockObject.SetActive(false);
        if(touchAbleUI != null){
            touchAbleUI.GetComponent<RectTransform>().anchoredPosition = savePos;
        }
    }
}
