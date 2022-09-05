using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INMoveControl : RectMove
{
    public INMoveControl instance;

    [Header("Const Variables")]
    private readonly List<List<Vector3>> INVENTORY_POSITION = new List<List<Vector3>>
    {
        new List<Vector3> {
            new Vector3(0f,   0f, 0f),
            new Vector3(0f, 200f, 0f)
        },
        new List<Vector3> {
            new Vector3(   0f, 0f, 0f),
            new Vector3(-200f, 0f, 0f)
        }
    };

    [Header("Variables")]
    private int curSceneNum;
    // setter
    public void SetSceneNum(int a_sceneNum) { curSceneNum = a_sceneNum - DefaultData.MAP_SCENE_IDX_NUM; }

    [Header("Coroutine Variables")]
    private IEnumerator coroutine;

    private new void Awake() {
        base.Awake();
        instance = this;
    }

    // called by ButtonControl
    public void ToggleInventoryPosition(bool a_isInvEnable) {
        if (coroutine != null) { StopCoroutine(coroutine); }

        Vector3 targetPosition = INVENTORY_POSITION[curSceneNum][a_isInvEnable ? 1 : 0];
        coroutine = SmoothMove(0.5f, targetPosition);

        StartCoroutine(coroutine);
    }

    // reset RectTransform's position
    public void ResetPosition() {
        rectTransform.anchoredPosition = new Vector3(0f, 0f, 0f);
    }
}
