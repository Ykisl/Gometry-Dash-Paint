using GeometryDashAPI;
using GeometryDashAPI.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBlockInfo : MonoBehaviour {
    public int ColorID;
    bool ActivePB;
    SpriteRenderer sr;
    public int Layer;
    //----------------
    public float OriginX;
    public float OriginY;
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
    public void RemovePB()
    {
        if (ActivePB == true)
        {
            LevelEditor.level.Blocks.Remove(LevelEditor.level.Blocks.Find(x => x.PositionX == OriginX & x.PositionY == OriginY));
            Destroy(gameObject);
        }
    }
}
