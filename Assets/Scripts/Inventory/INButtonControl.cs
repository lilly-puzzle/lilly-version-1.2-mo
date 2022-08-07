using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INButtonControl : MonoBehaviour
{
    [Header("Variables")]
    private bool isInvEnable;

    [Header("Script Variables")]
    [SerializeField] private INMoveControl inventoryMove;

    public void ToggleInventory() {
        isInvEnable = !isInvEnable;
        
        inventoryMove.ToggleInventoryPosition(isInvEnable);
    }

    public void ShiftSlot(int a_shiftDir) {
        // TODO: 인벤토리 슬롯 버튼에 대한 이벤트 처리
    }
}
