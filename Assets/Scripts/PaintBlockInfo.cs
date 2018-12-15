using GeometryDashAPI;
using GeometryDashAPI.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaintBlockInfo : MonoBehaviour {
    public int ColorID;
    bool ActivePB;
    public bool IsCursor;
    public bool CursorInEraceMode;
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
        if (CursorInEraceMode == true)
        {
            sr.color = new Color32(0, 0, 0, 155);
        }
        else
        {
            if (ActivePB == true)
            {
                sr.color = LevelEditor.GdColors[ColorID];
            }
            else
            {
                sr.color = new Color32(0, 0, 0, 0);
            }
        }
    }

     void PBRemoveControl()
    {
        if (LevelEditor.ModeSelectID == 3)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButton(0))
                {
                    RemovePB();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Cursor")
            {
                PBRemoveControl();
            }
        }
    }


    public void RemovePB()
    {
        if (ActivePB == true & IsCursor == false)
        {
            LevelEditor.level.Blocks.Remove(LevelEditor.level.Blocks.Find(x => x.PositionX == OriginX & x.PositionY == OriginY));
            Destroy(gameObject);
        }
    }

}
