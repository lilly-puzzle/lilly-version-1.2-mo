using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchManager : SimpleOneTouch
{
    [SerializeField] private float zoomPosX;
    [SerializeField] private float zoomPosY;
    [SerializeField] private float zoomSize;

    [SerializeField] private MapCameraManager mapCameraManager;


    protected override void FuncWhenTouchEnded() {
        if(!JoystickManager.instance.GetisJoystickAct()){
            mapCameraManager.ZoomIn(zoomPosX, zoomPosY, zoomSize);
        }
    }
}
