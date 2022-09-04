using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using C1ClockData;

public class C1ClockTouch : SimpleOneTouch
{
    [Header("Const Variables")]
    private int NUM_OF_IDX;
    private List<float> ROTATION_ANGLE;
    [SerializeField] private int CLOCK_TYPE;

    // [Header("Variables")]
    public int handIdx { get; private set; }

    [Header("Script Variables")]
    private C1ClockRotate rotateScript;
    private C1ClockActivate activateScript;

    private void Awake() {
        NUM_OF_IDX = CLOCK_DATA.NUM_OF_IDX_LIST[CLOCK_TYPE];
        ROTATION_ANGLE = CLOCK_DATA.ROTATION_ANGLE_LIST[CLOCK_TYPE];

        rotateScript = gameObject.GetComponent<C1ClockRotate>();
        if (CLOCK_TYPE == 1) activateScript = gameObject.GetComponent<C1ClockActivate>();
    }

    protected override void FuncWhenTouchEnded() {
        handIdx = (handIdx + 1) % NUM_OF_IDX;
        rotateScript.RotateHand(ROTATION_ANGLE[handIdx]);
        if (CLOCK_TYPE == 1) activateScript.SetColor(handIdx);
    }
}
