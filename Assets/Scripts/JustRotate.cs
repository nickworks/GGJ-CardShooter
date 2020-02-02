using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustRotate : MonoBehaviour
{
    public float speed = 90;
    public bool alsoBounce = true;
    void Update()
    {
        float degrees = speed * Time.deltaTime;

        transform.Rotate(0, degrees, 0);
        if (alsoBounce) {
            float y = Mathf.Sin(Time.time * 5) * .25f;
            transform.localPosition = new Vector3(0, y, 0);
        }
    }
}
