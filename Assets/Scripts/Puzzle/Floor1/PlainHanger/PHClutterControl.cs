using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHClutterControl : SimpleOneTouch
{
    [Header("Variables")]
    [SerializeField] private int clothesCode;

    protected override void FuncWhenTouchBegan() {
        PlainHanger.instance.StartToDragClothes(clothesCode);
        gameObject.SetActive(false);
    }
}
