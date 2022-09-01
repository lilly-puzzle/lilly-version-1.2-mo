using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVDialDrag : SimpleOneTouch
{
    [Header("Constant Variables")]
    private readonly List<float> DIAL_ANGLE = new List<float> {
        -45f, -90f, -135f, -180f, -225f, -270f, -315f, -360f, 0f
    };
    private readonly List<float> ANGLE_BOUNDARY = new List<float> {
        -22.5f, -67.5f, -112.5f, -157.5f, -202.5f, -247.5f, -292.5f, -337.5f, -382.5f
    };
    private const int NUM_OF_DIAL = 8;

    [Header("Variables")]
    private bool isDragging;
    private List<float> dialAngle;
    private int dialIdx;

    [Header("Script Variables")]
    private TVDialRotate rotateScript;

    private void Awake() {
        rotateScript = gameObject.GetComponent<TVDialRotate>();
    }

    private void LateUpdate() {
        if (!isDragging) return;

        if (touchPhase == touchPhaseEnded) {
            float angle = Mathf.Atan2(ray.y, ray.x) * Mathf.Rad2Deg;

            for (int i = 0; i < NUM_OF_DIAL; i++) {
                if (ANGLE_BOUNDARY[i] <= angle && angle < ANGLE_BOUNDARY[i + 1]) {
                    dialIdx = i;
                    break;
                }
            }

            rotateScript.RotateDial(DIAL_ANGLE[dialIdx]);
        }
    }

    private void FixedUpdate() {
        if (!isDragging) return;

        float angle = Mathf.Atan2(ray.y, ray.x) * Mathf.Rad2Deg;
        // float angle = Mathf.Atan2(ray.y, ray.x) * Mathf.Rad2Deg - startAngle;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    protected override void FuncWhenTouchBegan() {
        isDragging = true;
    }
}
