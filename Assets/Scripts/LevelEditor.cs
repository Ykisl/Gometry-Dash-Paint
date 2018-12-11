using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.GameObjects.Default;
using System;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour {
    public static int Zrange;
    public static int Group;
    public static int Layer;
    public static Vector2 MousePosition;
    public static Vector2 TMousePosition;
    public static float lastZ;
    public Text XPosTxt;
    public Text YPosTxt;
    public LocalLevels local;
    public static int EditColorId;
    ObjLibrary ol;
    BindLibrary bl;
    public Level level;
    public static string Exefile;
    public static int LevelNumber;
    //------------------------ColorPanels
    public Button ColorNext;
    public Button ColorPrew;
    public Button ThisColor;
    public Button ColorPanel1;
    public Button ColorPanel2;
    public Button ColorPanel3;
    public Button ColorPanel4;
    public Button ColorPanel5;
    public Button ColorPanel6;
    public Button ColorPanel7;
    public Button ColorPanel8;
    public Button ColorPanel9;
    public Button ColorPanel10;
    int ColorPanelid1 =1;
    int ColorPanelid2 =2;
    int ColorPanelid3 =3;
    int ColorPanelid4 =4;
    int ColorPanelid5 =5;
    int ColorPanelid6 =6;
    int ColorPanelid7 =7;
    int ColorPanelid8 =8;
    int ColorPanelid9 =9;
    int ColorPanelid10 =10;
    int ColorPanelX =0;
    public static Color32[] GdColors = new Color32[1001];
    ToolMode tm;
    string Levelname;
    public Button ArrowBtn;
    public Button BrushBtn;
    public Button SaveBtn;

    void Start () {
        Zrange = Convert.ToInt32(PlayerPrefs.GetString("ZRangeVarible"));
        Group = Convert.ToInt32(PlayerPrefs.GetString("GrupVarible"));
        Layer = Convert.ToInt32(PlayerPrefs.GetString("LayerVarible"));
        LevelNumber = PlayerPrefs.GetInt("edit_level");
        if (!PlayerPrefs.HasKey("ExeFile"))
        {
            Exefile = "GeometryDash";
        }
        else
        {
            Exefile = PlayerPrefs.GetString("ExeFile");
        }
        //-----------------------------------------------------------------
        ColorNext.onClick.AddListener(delegate { ColorPanelNavigatonAdd(); });
        ColorPrew.onClick.AddListener(delegate { ColorPanelNavigatonUndo(); });
        SaveBtn.onClick.AddListener(delegate { SaveLevel(); });
        ArrowBtn.onClick.AddListener(delegate { tm = ToolMode.Arow; });
        BrushBtn.onClick.AddListener(delegate { tm = ToolMode.Brush; });
        //-----------------------------------------------------------------
        string llpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + Exefile + "/CCLocalLevels.dat";
        local = new LocalLevels(llpath);
        ol = GetComponent<ObjLibrary>();
        bl = GetComponent<BindLibrary>();
        //-------------------------------
        LoadLevelData();
        LoadGDColors();
        ColorListenger();
        Levelname = local.Levels[LevelNumber].Name;
    }

    void ColorListenger()
    {
        ColorPanel1.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel1.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel2.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel2.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel3.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel3.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel4.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel4.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel5.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel5.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel6.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel6.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel7.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel7.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel8.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel8.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel9.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel9.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
        ColorPanel10.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel10.gameObject.GetComponentInChildren<Text>().text; lastZ = lastZ - 0.2f; });
    }

    void LoadGDColors()
    {
        for (short i = 0; i < 1000; i++)
        {
            GdColors[i] = new UnityEngine.Color32(level.Colors[i].Red, level.Colors[i].Green, level.Colors[i].Blue, (byte)(level.Colors[i].Opacity * 255));
        }
    }

    void LoadLevelData()
    {
        level = new Level(local.Levels[LevelNumber], bl.GetBindingLB(), bl.GetFilter());
        for(int i = 0; i < level.CountBlock; i++)
        {
            GameObject go = ol.SetGameObj(level.Blocks[i].ID);
            PaintBlockInfo pbi = go.GetComponent<PaintBlockInfo>();
            pbi.ColorID = level.Colors[(level.Blocks[i] as DetailBlock).ColorDetail].ID;
            go.transform.localScale = new Vector3(level.Blocks[i].Scale - 0.05f, level.Blocks[i].Scale - 0.05f, level.Blocks[i].Scale - 0.05f);
            if (go != null )
            {
                Instantiate(go, new Vector3(level.Blocks[i].PositionX /30, level.Blocks[i].PositionY/30, level.Blocks[i].ZOrder/10),Quaternion.identity);
            }
        }
    }

    void ColorPanelNavigatonAdd()
    {
        if (ColorPanelX != 99)
        {
            ColorPanelX++;
        }

    }
    void ColorPanelNavigatonUndo()
    {
        if(ColorPanelX != 0)
        {
            ColorPanelX--;
        }
    }
    void Update () {
        var cameraPosition = Camera.main.transform.position;

        float zWorldDistanceFromCamera = transform.position.z - cameraPosition.z;

        var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zWorldDistanceFromCamera);
        MousePosition = Camera.main.ScreenToViewportPoint(screenPoint) * 30;
        TMousePosition = Camera.main.ScreenToWorldPoint(screenPoint);
        XPosTxt.text = "X:" + MousePosition.x;
        YPosTxt.text = "Y:" + MousePosition.y;
        //----------------------------------------
        ColorPasher();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (tm == ToolMode.Arow)
            {


            }
            if (tm == ToolMode.Brush)
            {

                if (Input.GetMouseButton(0))
                {
                    AddDet(new Vector3(TMousePosition.x, TMousePosition.y, lastZ), 1887, Convert.ToInt32(ThisColor.gameObject.GetComponentInChildren<Text>().text), ol.SetGameObj(1887));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (tm == ToolMode.Arow)
        {

        }
        if (tm == ToolMode.Brush)
        {
            
        }
    }

    public enum ToolMode
    {
        Arow = 0,
        Brush = 1,
        Pencil = 2
    }

    void ColorPasher()
    {
        if (ColorPanelX < 0)
        {
            ColorPanelX = 0;
        }
        if (ColorPanelX > 99)
        {
            ColorPanelX = 99;
        }
        ColorPanelid1 = 1 + ColorPanelX * 10;
        ColorPanelid2 = 2 + ColorPanelX * 10;
        ColorPanelid3 = 3 + ColorPanelX * 10;
        ColorPanelid4 = 4 + ColorPanelX * 10;
        ColorPanelid5 = 5 + ColorPanelX * 10;
        ColorPanelid6 = 6 + ColorPanelX * 10;
        ColorPanelid7 = 7 + ColorPanelX * 10;
        ColorPanelid8 = 8 + ColorPanelX * 10;
        ColorPanelid9 = 9 + ColorPanelX * 10;
        ColorPanelid10 = 10 + ColorPanelX * 10;
        ColorPanel1.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid1);
        ColorPanel2.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid2);
        ColorPanel3.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid3);
        ColorPanel4.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid4);
        ColorPanel5.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid5);
        ColorPanel6.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid6);
        ColorPanel7.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid7);
        ColorPanel8.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid8);
        ColorPanel9.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid9);
        ColorPanel10.GetComponentInChildren<Text>().text = Convert.ToString(ColorPanelid10);

        if(Convert.ToInt32(ColorPanel1.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel1.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel1.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel2.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel2.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel2.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel3.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel3.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel3.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel4.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel4.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel4.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel5.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel5.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel5.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel6.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel6.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel6.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel7.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel7.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel7.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel8.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel8.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel8.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel9.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel9.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel9.gameObject.SetActive(true);
        }

        if (Convert.ToInt32(ColorPanel10.GetComponentInChildren<Text>().text) > 999)
        {
            ColorPanel10.gameObject.SetActive(false);
        }
        else
        {
            ColorPanel10.gameObject.SetActive(true);
        }

        ColorPanel1.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel1.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel2.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel2.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel3.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel3.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel4.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel4.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel5.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel5.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel6.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel6.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel7.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel7.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel8.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel8.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel9.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel9.gameObject.GetComponentInChildren<Text>().text)];
        ColorPanel10.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ColorPanel10.gameObject.GetComponentInChildren<Text>().text)];
        ThisColor.gameObject.GetComponent<Image>().color = GdColors[Convert.ToInt32(ThisColor.gameObject.GetComponentInChildren<Text>().text)];
        //---------------------------------------------------------------------------------
        
    }
    void AddDet(Vector3 position,int BlockId, int ColorID, GameObject Object)
    {
        Object.GetComponent<PaintBlockInfo>().ColorID = ColorID;
        Object.transform.localScale = new Vector3(1, 1, 1);
        Object.transform.localScale = new Vector3(Object.transform.localScale.x - 0.05f, Object.transform.localScale.y - 0.05f, Object.transform.localScale.z - 0.05f);
        Instantiate(Object, position, Quaternion.identity);
        level.AddBlock(new DetailBlock(BlockId)
        {
            PositionX = position.x * 30,
            PositionY = position.y * 30,
            ColorDetail = Convert.ToInt16(ColorID),
            ZOrder = Convert.ToInt16(Zrange + lastZ * 10),
            EditorL = Convert.ToInt16(Layer),
            Scale = Object.transform.localScale.x + 0.05f,
            
        });
        
    }

    void SaveLevel()
    {
        for (short i = 0; i < 1000; i++)
        {
            level.Colors[i] = new GeometryDashAPI.Levels.Color(i, GdColors[i].r, GdColors[i].g, GdColors[i].b);
            level.Colors[i].Opacity = GdColors[i].a / 255.0f;
        }
        Invoke("SaveLevelPart2", 1.0f);
        
    }

    public void SaveLevelPart2()
    {
        local.GetLevelByName(Levelname).LevelString = level.ToString();
        local.Save();
    }

}

