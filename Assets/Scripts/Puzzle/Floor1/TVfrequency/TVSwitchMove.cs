using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSwitchMove : ObjectMove
{
    [Header("Coroutine Variables")]
    private IEnumerator coroutine;

    public void MoveToPosition(Vector3 a_targetPosition) {
        if (coroutine != null) { StopCoroutine(coroutine); }
        coroutine = SmoothMove(1.5f, a_targetPosition);
        StartCoroutine(coroutine);
    }
}
