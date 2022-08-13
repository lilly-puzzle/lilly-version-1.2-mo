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

    private void FixedUpdate() {
        transform.position = touchPosWorld;
    }

    protected override void FuncWhenTouchEnded() {
        gameObject.SetActive(false);
    }
}
