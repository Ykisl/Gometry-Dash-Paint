using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGLModule : MonoBehaviour
{
    public Text ValueText;
    public static float BGLModyfy;
    public Slider sl;

    void Start()
    {
        sl = GetComponent<Slider>();
    }

    void Update()
    {
        BGLModyfy = sl.value;
        ValueText.text = Convert.ToString(BGLModyfy);
    }
}
