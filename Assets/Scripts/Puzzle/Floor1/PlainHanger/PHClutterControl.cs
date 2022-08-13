using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHClutterControl : SimpleOneTouch
{
    [Header("Variables")]
    [SerializeField] private int clothesCode;

    protected override void FuncWhenTouchBegan() {
        // TODO: 드래그 중인 옷의 sprite 변경 및 활성화
        gameObject.SetActive(false);
    }
}
