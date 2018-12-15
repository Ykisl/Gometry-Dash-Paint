using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolIndicator : MonoBehaviour {

    Image imagecomponent;
    public GameObject ScaleSlider;
    public Sprite Arrow;
    public Sprite Bush;
    public Sprite Eraser;
    void Start () {
        imagecomponent = GetComponent<Image>();
	}
	
	
	void Update () {
		if(LevelEditor.ModeSelectID == 0)
        {
            imagecomponent.sprite = Arrow;
            ScaleSlider.SetActive(false);
        }
        if(LevelEditor.ModeSelectID == 1)
        {
            imagecomponent.sprite = Bush;
            ScaleSlider.SetActive(true);
        }
        if (LevelEditor.ModeSelectID == 3)
        {
            imagecomponent.sprite = Eraser;
            ScaleSlider.SetActive(true);
        }
    }
}
