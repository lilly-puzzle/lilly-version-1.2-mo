using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLinkControl : MonoBehaviour
{
    
    [SerializeField] public int ItemCode;
    [SerializeField] private bool isOneTime;
    
    private void FuncWhenTouched(){
        //InventoryManager.AddItemInSlot(ItemCode);
        if (isOneTime){
            ItemLinkManager.instance.deleteItem(ItemCode);
            this.gameObject.SetActive(false);
        }
    }
}
