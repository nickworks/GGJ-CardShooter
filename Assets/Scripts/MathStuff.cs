using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MathStuff {
    public static float Damp(float a, float b, float percentLeftAfter1Second, float dt = 0) {
        if (dt == 0) dt = Time.deltaTime;
        return Lerp(a, b, 1 - Mathf.Pow(percentLeftAfter1Second, dt));
    }
    public static Vector3 Damp(Vector3 a, Vector3 b, float percentLeftAfter1Second, float dt = 0) {
        if (dt == 0) dt = Time.deltaTime;
        return Lerp(a, b, 1 - Mathf.Pow(percentLeftAfter1Second, dt));
    }
    public static Quaternion Damp(Quaternion a, Quaternion b, float percentLeftAfter1Second, float dt = 0) {
        if (dt == 0) dt = Time.deltaTime;
        return Lerp(a, b, 1 - Mathf.Pow(percentLeftAfter1Second, dt));
    }
    public static float Lerp(float a, float b, float p) {
        return (b - a) * p + a;
    }
    public static Vector3 Lerp(Vector3 a, Vector3 b, float p) {
        return (b - a) * p + a;
    }
    public static Quaternion Lerp(Quaternion a, Quaternion b, float p) {
        return Quaternion.Lerp(a, b, p);
    }
    public static float Map(float val, float min1, float max1, float min2, float max2) {
        float p = (val - min1) / (max1 - min1);
        return Lerp(min2, max2, p);
    }
}
