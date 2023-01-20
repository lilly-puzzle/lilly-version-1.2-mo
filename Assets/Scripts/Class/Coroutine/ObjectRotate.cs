using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    // rotate coroutine
    protected IEnumerator SmoothRotation(float a_duration, Quaternion a_targetAngle) {
        Quaternion startAng = transform.rotation;
        Quaternion finalAng = a_targetAngle;
        float elapsedTime = 0f;
        
        while (elapsedTime < a_duration) {
            float rate = elapsedTime / a_duration;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);

            transform.rotation = Quaternion.Slerp(startAng, finalAng, rate);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
