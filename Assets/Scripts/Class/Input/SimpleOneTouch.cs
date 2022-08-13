using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOneTouch : MonoBehaviour
{
    /// <see href="https://answers.unity.com/questions/1126621/best-way-to-detect-touch-on-a-gameobject.html"/>

    protected Vector3 touchPosWorld;
 
    private const TouchPhase touchPhaseBegan = TouchPhase.Began;
    private const TouchPhase touchPhaseMoved = TouchPhase.Moved;
    private const TouchPhase touchPhaseEnded = TouchPhase.Ended;

    private void Update() {
        if (Input.touchCount <= 0) return;

        TouchPhase getTouchPhase = Input.GetTouch(0).phase;

        touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

        RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

        if (hitInformation.collider == null) return;

        GameObject touchedObject = hitInformation.transform.gameObject;

        if (touchedObject == this.gameObject) {
            if (getTouchPhase == touchPhaseBegan) {
                FuncWhenTouchBegan();
            } else if (getTouchPhase == touchPhaseMoved) {
                FuncWhenTouchMoved();
            } else if (getTouchPhase == touchPhaseEnded) {
                FuncWhenTouchEnded();
            }
        }
    }

    protected virtual void FuncWhenTouchBegan() { }
    protected virtual void FuncWhenTouchMoved() { }
    protected virtual void FuncWhenTouchEnded() { }
}
