using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MathStuff {
    public static float Damp(float a, float b, float percentLeftAfter1Second) {
        return Lerp(a, b, 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime));
    }
    public static Vector3 Damp(Vector3 a, Vector3 b, float percentLeftAfter1Second) {
        return Lerp(a, b, 1 - Mathf.Pow(percentLeftAfter1Second, Time.deltaTime));
    }
    public static float Lerp(float a, float b, float p) {
        return (b - a) * p + a;
    }
    public static Vector3 Lerp(Vector3 a, Vector3 b, float p) {
        return (b - a) * p + a;
    }
}
