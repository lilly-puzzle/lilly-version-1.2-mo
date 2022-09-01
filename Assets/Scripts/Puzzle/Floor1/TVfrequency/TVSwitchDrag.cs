using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSwitchDrag : SimpleOneTouch
{
    [Header("Constant Variables")]
    private readonly List<float> SWITCH_LOC = new List<float> {
        // TODO: position 좌표 추가
    };
    private readonly List<float> POS_BOUNDARY = new List<float> {
        // TODO: position 좌표 추가
    };

    [Header("Variables")]
    private bool isDragging;
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
                case float val when (SWITCH_LOC[0] <= val && val <= POS_BOUNDARY[0]): {
                    switchIdx = 0;
                    break;
                }
                case float val when (POS_BOUNDARY[0] < val && val < POS_BOUNDARY[1]): {
                    switchIdx = 1;
                    break;
                }
                case float val when (POS_BOUNDARY[1] <= val && val <= SWITCH_LOC[2]): {
                    switchIdx = 2;
                    break;
                }
                default: {
                    switchIdx = -1;
                    break;
                }
            }

            Vector3 temp = transform.position;
            temp.y = SWITCH_LOC[switchIdx];
            moveScript.MoveToPosition(temp);
        }
    }

    private void FixedUpdate() {
        if (!isDragging) return;

        float yCoor = ray.y;

        if (yCoor < POS_BOUNDARY[0]) yCoor = POS_BOUNDARY[0];
        else if (POS_BOUNDARY[1] < yCoor) yCoor = POS_BOUNDARY[1];

        Vector3 temp = transform.position;
        temp.y = yCoor;
        transform.position = temp;
    }

    protected override void FuncWhenTouchBegan() {
        isDragging = true;
    }
}
