using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseTowardsTarget : MonoBehaviour {
    public Transform target;
    void Update() {
        if (target != null) {
            transform.position = MathStuff.Damp(transform.position, target.position, .1f);
        }
    }
}