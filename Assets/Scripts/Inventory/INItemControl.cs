using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INItemControl : MonoBehaviour
{
    [Header("Variables")]
    private int itemCode = -1;

    [Header("Sprite Variables")]
    private Image itemImage;

    private void Awake() {
        itemImage = GetComponent<Image>();
    }

    // Function executed by button event
    public void InputItem() {
        InventoryManager.instance.SelectItem(itemCode);
    }

    // Item value setting by external call
    private void SetItemAlpha(float a_aValue) {
        var tempColor = itemImage.color;
        tempColor.a = a_aValue;
        itemImage.color = tempColor;
    }

    private void SetItemSprite(Sprite a_itemSprite) {
        itemImage.sprite = a_itemSprite;
    }

    public void SetItem(int a_itemCode, Sprite a_itemSprite) {
        itemCode = a_itemCode;

        if (a_itemCode == -1) {
            SetItemAlpha(0f);
        } else {
            SetItemSprite(a_itemSprite);
            SetItemAlpha(1f);
        }
    }
}
