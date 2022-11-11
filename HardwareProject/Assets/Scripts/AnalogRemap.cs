using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogRemap
{
   float Lerp(float a,float b, float t)
    {
        return (1.0f - t) * a + t * b;
    }

    float InverseLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }

    public float Remap(float aMin,float aMax, float bMin, float bMax, float v)
    {
        float t = InverseLerp(aMin, aMax, v);
        return Lerp(bMin, bMax, t);
    }
}

//Inverse Lerp code provided by
//https://www.gamedev.net/articles/programming/general-and-gameplay-programming/inverse-lerp-a-super-useful-yet-often-overlooked-function-r5230/