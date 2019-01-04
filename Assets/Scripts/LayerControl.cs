using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerControl : MonoBehaviour {

    InputField inful;
    public int SelectLayer;
	void Start () {
        inful = GetComponent<InputField>();
	}
	
	
	void Update () {
        SelectLayer = Convert.ToInt32(inful.text);
        if (SelectLayer > 100) SelectLayer = 100;
        if (SelectLayer < -100) SelectLayer = -100;
        inful.text = Convert.ToString(SelectLayer);
    }

    public void ChangeSelectVarible(int mode)
    {
        if(mode == 1)
        {
            SelectLayer++;
            if(SelectLayer > -3 & SelectLayer < 0)
            {
                SelectLayer = 3; 
            }
            inful.text = Convert.ToString(SelectLayer);
        }
        if(mode == -1)
        {
            SelectLayer--;
            if (SelectLayer < 3 & SelectLayer > 0)
            {
                SelectLayer = -3;
            }
            inful.text = Convert.ToString(SelectLayer);
        }
    }
}
