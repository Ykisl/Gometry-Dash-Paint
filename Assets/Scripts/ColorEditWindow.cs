using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorEditWindow : MonoBehaviour {

    ColorPicker cp;
    int ColorID;
    public Button ThisColor;
    public Button Save;
    public Button Ok;
    bool Loaded;
	void Start () {
        Save.onClick.AddListener(delegate { SaveBtn(); });
        Ok.onClick.AddListener(delegate { OkBtn(); });
        Loaded = true;

    }
	
    public void ChangeColors()
    {
        cp = GetComponentInChildren<ColorPicker>();
        ColorID = LevelEditor.EditColorId;
        cp.CurrentColor = LevelEditor.GdColors[ColorID];
    }

    void SaveBtn()
    {
        LevelEditor.GdColors[ColorID] = cp.CurrentColor;
    }

    void OkBtn()
    {
        LevelEditor.GdColors[ColorID] = cp.CurrentColor;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Loaded == true)
        {
            ThisColor.gameObject.GetComponent<Image>().color = cp.CurrentColor;
            ThisColor.gameObject.GetComponentInChildren<Text>().text = Convert.ToString(ColorID);
        }
	}
}
