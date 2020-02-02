using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TomeGUI : MonoBehaviour
{
    /// <summary>
    /// A reference to the tome model this gui represents
    /// </summary>
    public Tome tome;

    Quaternion targetRotation;
    Vector2 targetPosition;
    float targetScale = 1;
    RectTransform rt;

    Image img;
    public Sprite bookClosed;
    public Sprite bookOpen;
    public TextMeshProUGUI txtCards;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        rt = transform as RectTransform;
    }
    public void Init(Tome tome) {
        this.tome = tome;
    }

    // Update is called once per frame
    void Update()
    {
        EasePositionRotationScale();
        txtCards.text = $"{tome.cards.Count} / {tome.cardCap}";
    }
    private void EasePositionRotationScale() {
        float percentRemainingAfter1Second = .001f;
        //rt.localRotation = MathStuff.Damp(transform.localRotation, targetRotation, percentRemainingAfter1Second);
        rt.anchoredPosition = MathStuff.Damp(rt.anchoredPosition, targetPosition, percentRemainingAfter1Second);
        transform.localScale = Vector3.one * MathStuff.Damp(transform.localScale.x, targetScale, percentRemainingAfter1Second);
    }
    public void AnimateTo(Vector3 pos, bool isCurrent) {
        targetPosition = pos;
        targetScale = isCurrent ? .5f : .25f;
        if (img == null) img = GetComponent<Image>();
        if (img != null) img.sprite = isCurrent ? bookOpen : bookClosed;
    }
}
