using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        RaycastHit2D hit = Physics2D.Raycast(ray, Vector3.forward);
        col = null;
        if (hit.collider != null) {
            col = hit.transform.gameObject;

            if (col == this.gameObject) {
                switch (touchPhase) {
                    case touchPhaseBegan: {
                        if(IsPointerOverUIObject() == false){
                            FuncWhenTouchBegan();
                        }
                        break;
                    }
                    case touchPhaseMoved: {
                        FuncWhenTouchMoved();
                        break;
                    }
                    case touchPhaseEnded: {
                        if(IsPointerOverUIObject() == false){
                            FuncWhenTouchEnded();
                        }
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

    private bool IsPointerOverUIObject(){
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
