using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath
{
    public static float SmoothLerp(float from, float to, float sharpness)
    {
        return Mathf.Lerp(from, to, 1f - Mathf.Exp(-sharpness * Time.deltaTime));
    }
    
    public static Vector3 SmoothLerp(Vector3 from, Vector3 to, float sharpness)
    {
        return Vector3.Lerp(from, to, 1f - Mathf.Exp(-sharpness * Time.deltaTime));
    }
    
    public static Color SmoothLerp(Color from, Color to, float sharpness)
    {
        return Color.Lerp(from, to, 1f - Mathf.Exp(-sharpness * Time.deltaTime));
    }
}
