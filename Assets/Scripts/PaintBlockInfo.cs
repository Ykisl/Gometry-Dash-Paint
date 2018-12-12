using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBlockInfo : MonoBehaviour {
    public int ColorID;
    bool ActivePB;
    SpriteRenderer sr;
    public int Layer;
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        
	}
	
	
	void Update () {
        if (LevelEditor.ShowLayerGO == false) {
            if (ActivePB == true & Layer != LevelEditor.GameObjLayer)
            {
                ActivePB = false;
            }
            if (ActivePB == false & Layer == LevelEditor.GameObjLayer)
            {
                ActivePB = true;
            }
        }
        else
        {
            if (ActivePB == false)
            {
                ActivePB = true;
            }
        }
        PBControl();
	}

    void PBControl()
    {
        if(ActivePB == true)
        {
            sr.color = LevelEditor.GdColors[ColorID];
        }
        else
        {
            sr.color = new Color32(0, 0, 0, 0);
        }
    }
}
