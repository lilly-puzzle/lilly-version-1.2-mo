using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSwitchDrag : SimpleOneTouch
{
    [Header("Variables")]
    private bool isDragging;
    private int lastSwitchIdx;

    private void LateUpdate() {
        if (!isDragging) return;

        if (touchPhase == touchPhaseMoved) {
            // TODO: col로 하위 object가 있는지 확인
        } else if (touchPhase == touchPhaseEnded) {
            // TODO: 가장 최근에 선택된 스위치 위치로 이동
        }
    }

    private void FixedUpdate() {
        if (!isDragging) return;

        // TODO: object의 position 변경 (touch의 y좌표만 사용)
    }

    protected override void FuncWhenTouchBegan() {
        isDragging = true;
    }
}
