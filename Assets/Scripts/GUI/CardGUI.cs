using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGUI : MonoBehaviour {

    Quaternion targetRotation;
    Vector2 targetPosition;
    float targetScale = 1;
    float delay = 0;

    RectTransform rt;
    void Start() {
        rt = transform as RectTransform;
    }
    public void SetDepth(int i) {
        GetComponent<Canvas>().sortingOrder = i;
    }
    public void Tint() {
        GetComponent<Image>().color = Color.red;
    }
    void Update() {
        if (delay > 0) {
            delay -= Time.deltaTime;
            return;
        }
        rt.localRotation = MathStuff.Damp(transform.localRotation, targetRotation, .01f);
        rt.anchoredPosition = MathStuff.Damp(rt.anchoredPosition, targetPosition, .001f);
        transform.localScale = Vector3.one * MathStuff.Damp(transform.localScale.x, targetScale, .01f);
    }
    public void AnimateTo(Vector3 pos, Quaternion rot, float scale, float delay) {
        targetPosition = pos;
        targetRotation = rot;
        targetScale = scale;
        this.delay = delay;
    }
}
