using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVProgressControl : ObjectMove
{
    [Header("Constant Variables")]
    private readonly List<Vector3> PROGRESS_BUTTON_POS = new List<Vector3> {
        new Vector3(-4.792f, -2.835f, 0.0f),
        new Vector3(-4.010f, -2.835f, 0.0f),
        new Vector3(-1.688f, -2.835f, 0.0f),
        new Vector3( 0.467f, -2.835f, 0.0f),
        new Vector3( 1.407f, -2.835f, 0.0f)
    };

    [Header ("Coroutine Variables")]
    private IEnumerator coroutine;

    public void MoveToPosition(int a_moveIdx) {
        if (coroutine != null) { StopCoroutine(coroutine); }
        coroutine = SmoothMove(1.5f, PROGRESS_BUTTON_POS[a_moveIdx]);
        StartCoroutine(coroutine);
    }
}
