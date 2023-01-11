using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHDraggingControl : SimpleOneTouch
{
    // [Header("Variables")]
    public static int clothesCode { get; private set; }

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
            PlainHanger.instance.FailToDragClothes(clothesCode);
        } else {
            PlainHanger.instance.TryToHangClothes(col, clothesCode);
        }
        clothesCode = -1;
        gameObject.SetActive(false);
    }

    private void FixedUpdate() {
        transform.position = ray;
    }

    public void SetClothes(int a_clothesCode, Sprite a_sprite) {
        clothesCode = a_clothesCode;
        SetSprite(a_sprite);
    }
}
