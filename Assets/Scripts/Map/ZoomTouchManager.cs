using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTouchManager : MonoBehaviour
{
    [SerializeField] private float zoomPosX;
    [SerializeField] private float zoomPosY;
    [SerializeField] private float zoomSize;

    [SerializeField] private MapCameraManager mapCameraManager;

    // SimpleOneTouch
    void SimpleOneTouch(){
        mapCameraManager.ZoomIn(zoomPosX, zoomPosY, zoomSize);
    }
}
