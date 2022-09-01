using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVLedControl : MonoBehaviour
{
    [Header("Sprite Variables")]
    private readonly Color greenColor = new Color(0.259078f, 0.7735849f, 0.261645f);
    private readonly Color grayColor = new Color(0.1037736f, 0.1037736f, 0.1037736f);
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetColor(bool a_isClear) {
        if (a_isClear) {
            spriteRenderer.color = greenColor;
        } else {
            spriteRenderer.color = grayColor;
        }
    }
}
