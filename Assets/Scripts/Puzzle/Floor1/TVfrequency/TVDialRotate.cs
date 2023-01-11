using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVDialRotate : ObjectRotate
{
    [Header("Coroutine Variables")]
    private IEnumerator coroutine;

    public void RotateDial(float a_targetAngle) {
        Quaternion target = Quaternion.AngleAxis(a_targetAngle, Vector3.forward);

        if (coroutine != null) { StopCoroutine(coroutine); }
        coroutine = SmoothRotation(0.5f, target);
        StartCoroutine(coroutine);
    }
}
