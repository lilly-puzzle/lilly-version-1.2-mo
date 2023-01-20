using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1ClockActivate : MonoBehaviour
{
    [Header("Const Variables")]
    private readonly Color LIGHT_COLOR = new Color(0f, 0f, 0f);
    private readonly Color DARK_COLOR = new Color(0.1037736f, 0.1037736f, 0.1037736f);

    [Header("Sprite Variables")]
    [SerializeField] private SpriteRenderer amSprite;
    [SerializeField] private SpriteRenderer pmSprite;

    public void SetColor(int a_handIdx) {
        if (a_handIdx == 0) {
            amSprite.color = LIGHT_COLOR;
            pmSprite.color = DARK_COLOR;
        } else if (a_handIdx == 1) {
            amSprite.color = DARK_COLOR;
            pmSprite.color = LIGHT_COLOR;
        }
    }
}
