using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOneTouch : MonoBehaviour
{
    /// <see href="https://answers.unity.com/questions/1126621/best-way-to-detect-touch-on-a-gameobject.html"/>
    /// <see href="https://docs.unity3d.com/ScriptReference/Physics.Raycast.html"/>

    protected Vector3 ray;
    protected GameObject col;
    protected TouchPhase touchPhase;
 
    protected const TouchPhase touchPhaseBegan = TouchPhase.Began;
    protected const TouchPhase touchPhaseMoved = TouchPhase.Moved;
    protected const TouchPhase touchPhaseEnded = TouchPhase.Ended;

    private void Update() {
        if (Input.touchCount <= 0) return;

        touchPhase = Input.GetTouch(0).phase;
        ray = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        RaycastHit hit;
        col = null;
        if (Physics.Raycast(ray, Vector3.forward, out hit)) {
            col = hit.transform.gameObject;

            if (col == this.gameObject) {
                switch (touchPhase) {
                    case touchPhaseBegan: {
                        FuncWhenTouchBegan();
                        break;
                    }
                    case touchPhaseMoved: {
                        FuncWhenTouchMoved();
                        break;
                    }
                    case touchPhaseEnded: {
                        FuncWhenTouchEnded();
                        break;
                    }
                    default: {
                        break;
                    }
                }
            }
        }
    }

    protected virtual void FuncWhenTouchBegan() { }
    protected virtual void FuncWhenTouchMoved() { }
    protected virtual void FuncWhenTouchEnded() { }
}
