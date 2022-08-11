using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLinkControl : MonoBehaviour
{
    
    [SerializeField] public int ItemCode;
    [SerializeField] private bool isOneTime;

    void OnMouseUp(){
        if(!JoystickManager.instance.GetisJoystickAct()){
            InventoryManager.instance.PushItem(ItemCode);
            if (isOneTime){
                ItemLinkManager.instance.deleteItem(ItemCode);
                this.gameObject.SetActive(false);
            }
        }
    }
}
