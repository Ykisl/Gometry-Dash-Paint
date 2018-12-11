using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPanel : MonoBehaviour, IPointerClickHandler{
    Text text;
    public GameObject WindowColorPanel;
    void Start () {
        text = this.gameObject.GetComponentInChildren<Text>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(WindowColorPanel.active == true)
            {
                WindowColorPanel.SetActive(true);
                LevelEditor.EditColorId = Convert.ToInt32(text.text);
                WindowColorPanel.gameObject.GetComponent<ColorEditWindow>().ChangeColors();
            }
            else
            {
                WindowColorPanel.SetActive(true);
                WindowColorPanel.gameObject.GetComponent<RectTransform>().position = new Vector2(365.88f, 255.26f);
                LevelEditor.EditColorId = Convert.ToInt32(text.text);
                WindowColorPanel.gameObject.GetComponent<ColorEditWindow>().ChangeColors();
            }
        }

    }

    void Update () {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }
}
