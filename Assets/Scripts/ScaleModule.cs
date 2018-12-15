using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleModule : MonoBehaviour {
    public Text ValueText;
    public static float ScaleModyfy;
    Slider sl;

	void Start () {
        sl = GetComponent<Slider>();
	}
	
	void Update () {
        ScaleModyfy = sl.value;
        ValueText.text = Convert.ToString(ScaleModyfy);
	}
}
