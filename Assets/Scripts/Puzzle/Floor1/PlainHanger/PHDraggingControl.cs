using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHDraggingControl : SimpleOneTouch
{
    // [Header("Variables")]
    public static int clothesCode { get; private set; }
    // setter
    public void SetClothes(int a_clothesCode) { clothesCode = a_clothesCode; }

    [Header("Sprite Variables")]
    private SpriteRenderer spriteRenderer;
    // setter
    public void SetSprite(Sprite a_sprite) { spriteRenderer.sprite = a_sprite; }

    private void Awake() {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        if (touchPhase != touchPhaseEnded) return;

        PHHangerControl hanger = null;
        if (col != null) hanger = col.GetComponent<PHHangerControl>();

        if (hanger == null) {
            // TODO: clutter 재활성화 & dragging 비활성화
        } else {
            // TODO: 옷걸기
        }
    }

    private void FixedUpdate() {
        transform.position = ray;
    }
}
