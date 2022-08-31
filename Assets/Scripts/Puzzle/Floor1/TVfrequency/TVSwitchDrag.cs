using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSwitchDrag : SimpleOneTouch
{
    [Header("Variables")]
    private bool isDragging;
    private List<float> switchLoc;
    private List<float> moveBoundary;
    private int switchIdx;

    [Header("Script Variables")]
    private TVSwitchMove moveScript;

    private void Awake() {
        moveScript = gameObject.GetComponent<TVSwitchMove>();
    }

    private void LateUpdate() {
        if (!isDragging) return;

        if (touchPhase == touchPhaseEnded) {
            switch(ray.y) {
                case float val when (switchLoc[0] <= val && val <= moveBoundary[0]): {
                    switchIdx = 0;
                    break;
                }
                case float val when (moveBoundary[0] < val && val < moveBoundary[1]): {
                    switchIdx = 1;
                    break;
                }
                case float val when (moveBoundary[1] <= val && val <= switchLoc[2]): {
                    switchIdx = 2;
                    break;
                }
                default: {
                    switchIdx = -1;
                    break;
                }
            }

            Vector3 temp = transform.position;
            temp.y = switchLoc[switchIdx];
            moveScript.MoveToPosition(temp);
        }
    }

    private void FixedUpdate() {
        if (!isDragging) return;

        float yCoor = ray.y;

        if (yCoor < moveBoundary[0]) yCoor = moveBoundary[0];
        else if (moveBoundary[1] < yCoor) yCoor = moveBoundary[1];

        Vector3 temp = transform.position;
        temp.y = yCoor;
        transform.position = temp;
    }

    protected override void FuncWhenTouchBegan() {
        isDragging = true;
    }
}
