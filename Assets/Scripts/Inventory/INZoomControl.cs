using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class INZoomControl : MonoBehaviour
{
    [Header("Sprite Variables")]
    private Image zoomImage;

    private void Awake() {
        zoomImage = GetComponent<Image>();
    }

    public void ZoomItem(Sprite a_itemSprite) {
        zoomImage.sprite = a_itemSprite;
    }

    public void InputZoomedItem() {
        gameObject.SetActive(false);
    }
}
