using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class INMoveControl : RectMove
{
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

    [Header("Coroutine Variables")]
    private IEnumerator coroutine;

    public void SetSceneNum() {
        curSceneNum = SceneManager.GetActiveScene().buildIndex - DefaultData.MAP_SCENE_IDX_NUM;
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
