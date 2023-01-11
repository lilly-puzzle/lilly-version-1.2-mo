using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    // move coroutine
    protected IEnumerator SmoothMove(float a_duration, Vector3 a_targetPosition) {
        Vector3 startPos = transform.position;
        Vector3 finalPos = a_targetPosition;
        float elapsedTime = 0f;
        
        while (elapsedTime < a_duration) {
            float rate = elapsedTime / a_duration;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);

            transform.position = Vector3.Lerp(startPos, finalPos, rate);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
