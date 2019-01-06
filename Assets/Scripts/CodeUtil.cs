using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeUtil : MonoBehaviour
{
    public static float StringToFloat(string str)
    {
        return (float)Convert.ToDouble(str);
    }

    public static Byte StringToByte(string str)
    {
        return (Byte)Convert.ToInt32(str);
    }

    public static Byte FloatToByte(float str)
    {
        return (Byte)Convert.ToInt32(str);
    }


}
