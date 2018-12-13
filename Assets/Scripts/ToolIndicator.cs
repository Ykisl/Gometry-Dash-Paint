using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolIndicator : MonoBehaviour {

    Image imagecomponent;
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
        }
        if(LevelEditor.ModeSelectID == 1)
        {
            imagecomponent.sprite = Bush;
        }
        if (LevelEditor.ModeSelectID == 3)
        {
            imagecomponent.sprite = Eraser;
        }
    }
}
