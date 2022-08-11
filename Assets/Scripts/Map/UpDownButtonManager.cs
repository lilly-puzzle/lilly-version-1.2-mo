using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownButtonManager : MonoBehaviour
{
    [Header("Script Varibale")]
    [SerializeField] private MoveManager moveManager;
    
    [Header("Object Variable")]
    [SerializeField] private GameObject upButtonObject;
    [SerializeField] private GameObject downButtonObject;


    void Awake(){
        upButtonObject.SetActive(false);
        downButtonObject.SetActive(false);
    }

    public void makeButton(bool isUpButton){
        if(isUpButton){
            upButtonObject.SetActive(true);
        }
        else{
            downButtonObject.SetActive(true);
        }
    }
    
    public void deleteButton(bool isUpButton){
        if(isUpButton){
            upButtonObject.SetActive(false);
        }
        else{
            downButtonObject.SetActive(false);
        }
    }

    public void UpDownButtonPress(bool isUpButton){
        if(isUpButton){
            moveManager.MoveFloor(true);
        }
        else{
            moveManager.MoveFloor(false);
        }
    }

}
