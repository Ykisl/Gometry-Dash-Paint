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
    public static int GameObjLayer;
    public static Vector2 MousePosition;
    public static Vector2 TMousePosition;
    public Text XPosTxt;
    public Text YPosTxt;
    public LocalLevels local;
    public static int EditColorId;
    ObjLibrary ol;
    BindLibrary bl;
    public static Level level;
    public static string Exefile;
    public static int LevelNumber;
    public static bool ShowLayerGO;
    public static int ModeSelectID;
    public Vector3 cameraPosition;
    GameObject BushCusor;
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
    public Button EraserBtn;
    public Button SaveBtn;
    public Button CloseBtn;
    public Button SettingsBtn;
    public GameObject SettingsWindow;
    public InputField LayerInput;
    public Toggle isVisiblePB;
    public SpriteRenderer BGrender;
    public GameObject SaveEventPanel;
    public GameObject CloseEventPanel;
    public SaveTextModule STM;
    byte pashe = 0;
    Vector3 CursorObjLastPos;
    public float CursorObjSpeed;
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
        SettingsBtn.onClick.AddListener(delegate { SettingsEvent(); });
        CloseBtn.onClick.AddListener(delegate { CloseEventPanel.SetActive(true); });
        ArrowBtn.onClick.AddListener(delegate { tm = ToolMode.Arow; });
        BrushBtn.onClick.AddListener(delegate { tm = ToolMode.Brush; });
        EraserBtn.onClick.AddListener(delegate { tm = ToolMode.Eraser; });
        //-----------------------------------------------------------------
        string llpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + Exefile + "/CCLocalLevels.dat";
        local = new LocalLevels(llpath);
        ol = GetComponent<ObjLibrary>();
        bl = GetComponent<BindLibrary>();
        BushCusor = Instantiate(ol.SetGameObj(-2), new Vector3(TMousePosition.x, TMousePosition.y, -300f),Quaternion.identity);
        //-------------------------------
        cameraPosition = Camera.main.transform.position;
        LoadLevelData();
        LoadGDColors();
        ColorListenger();
        Levelname = local.Levels[LevelNumber].Name;
        CursorObjLastPos = Vector3.zero;
    }

    void ColorListenger()
    {
        ColorPanel1.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel1.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel2.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel2.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel3.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel3.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel4.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel4.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel5.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel5.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel6.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel6.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel7.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel7.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel8.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel8.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel9.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel9.gameObject.GetComponentInChildren<Text>().text;});
        ColorPanel10.onClick.AddListener(delegate { ThisColor.gameObject.GetComponentInChildren<Text>().text = ColorPanel10.gameObject.GetComponentInChildren<Text>().text;});
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
            pbi.Layer = level.Blocks[i].ZOrder;
            go.transform.localScale = new Vector3(level.Blocks[i].Scale - 0.11f, level.Blocks[i].Scale - 0.11f, 1);
            go.GetComponent<PaintBlockInfo>().OriginX = level.Blocks[i].PositionX;
            go.GetComponent<PaintBlockInfo>().OriginY = level.Blocks[i].PositionY;
            if (go != null )
            {
                Instantiate(go, new Vector3(level.Blocks[i].PositionX / 30, level.Blocks[i].PositionY / 30, level.Blocks[i].ZOrder /-1f),Quaternion.identity);
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
        

        float zWorldDistanceFromCamera = transform.position.z - cameraPosition.z;

        var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zWorldDistanceFromCamera);
        MousePosition = Camera.main.ScreenToViewportPoint(screenPoint) * 30;
        TMousePosition = Camera.main.ScreenToWorldPoint(screenPoint);
        XPosTxt.text = "X:" + TMousePosition.x * 30;
        YPosTxt.text = "Y:" + TMousePosition.y * 30;
        //----------------------------------------
        ColorPasher();
        

      

        GameObjLayer = Convert.ToInt32(LayerInput.text);
        ShowLayerGO = isVisiblePB.isOn;
        ModeSelectID = (int)tm;
    }

    private void LateUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (tm == ToolMode.Arow)
            {


            }
            if (tm == ToolMode.Brush)
            {

                BushCusor.transform.localScale = new Vector3(ScaleModule.ScaleModyfy - 0.11f, ScaleModule.ScaleModyfy - 0.11f, ScaleModule.ScaleModyfy - 0.11f);
                BushCusor.transform.position = new Vector3(TMousePosition.x, TMousePosition.y, -300f);

                if (Input.GetMouseButton(0))
                {
                    
                    if (CursorObjSpeed > 0 & CursorObjSpeed < 3)
                    {
                        if (pashe == 0)
                        {
                            AddDet(new Vector3(TMousePosition.x, TMousePosition.y, GameObjLayer / -1f), 1887, Convert.ToInt32(ThisColor.gameObject.GetComponentInChildren<Text>().text), ol.SetGameObj(1887));
                            pashe = 1;
                        }
                        else
                        {
                            pashe = 0;
                        }
                    }
                    else if (CursorObjSpeed > 3)
                    {
                        AddDet(new Vector3(TMousePosition.x, TMousePosition.y, GameObjLayer / -1f), 1887, Convert.ToInt32(ThisColor.gameObject.GetComponentInChildren<Text>().text), ol.SetGameObj(1887));
                    }
                    else if (CursorObjSpeed < 1)
                    {
                        AddDet(new Vector3(TMousePosition.x, TMousePosition.y, GameObjLayer / -1f), 1887, Convert.ToInt32(ThisColor.gameObject.GetComponentInChildren<Text>().text), ol.SetGameObj(1887));
                    }
                }
            }
            if (tm == ToolMode.Eraser)
            {
                BushCusor.transform.localScale = new Vector3(ScaleModule.ScaleModyfy - 0.11f, ScaleModule.ScaleModyfy - 0.11f, ScaleModule.ScaleModyfy - 0.11f);
                BushCusor.transform.position = new Vector3(TMousePosition.x, TMousePosition.y, -300f);
                if (Input.GetMouseButton(0))
                {
                    
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            pashe = 1;
        }
    }


    private void FixedUpdate()
    {
        CursorObjSpeed = (BushCusor.transform.position - CursorObjLastPos).magnitude * 30;
        CursorObjLastPos = BushCusor.transform.position;

        if (tm == ToolMode.Arow)
        {
            BushCusor.GetComponent<PaintBlockInfo>().CursorInEraceMode = false;
            BushCusor.SetActive(false);
        }
        if (tm == ToolMode.Brush)
        {
            if (Input.GetMouseButtonUp(0))
            {
                pashe = 0;
            }
            BushCusor.GetComponent<PaintBlockInfo>().CursorInEraceMode = false;
            BushCusor.transform.localScale = new Vector3(ScaleModule.ScaleModyfy - 0.11f, ScaleModule.ScaleModyfy - 0.11f, ScaleModule.ScaleModyfy - 0.11f);
            BushCusor.GetComponent<PaintBlockInfo>().ColorID = Convert.ToInt32(ThisColor.gameObject.GetComponentInChildren<Text>().text);
            BushCusor.SetActive(true);
        }
        if(tm == ToolMode.Eraser)
        {
            BushCusor.GetComponent<PaintBlockInfo>().CursorInEraceMode = true;
            BushCusor.SetActive(true);
        }
    }


    public enum ToolMode
    {
        Arow = 0,
        Brush = 1,
        Pencil = 2,
        Eraser = 3
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
        Object.GetComponent<PaintBlockInfo>().Layer = GameObjLayer;
        Object.transform.localScale = new Vector3(ScaleModule.ScaleModyfy, ScaleModule.ScaleModyfy, 1);
        Object.transform.localScale = new Vector3(Object.transform.localScale.x - 0.11f, Object.transform.localScale.y - 0.11f, 1);
        Object.GetComponent<PaintBlockInfo>().OriginX = position.x * 30;
        Object.GetComponent<PaintBlockInfo>().OriginY = position.y * 30;
        Instantiate(Object, position, Quaternion.identity);
        level.AddBlock(new DetailBlock(BlockId)
        {
            PositionX = position.x * 30,
            PositionY = position.y * 30,
            ColorDetail = Convert.ToInt16(ColorID),
            ZOrder = (short)GameObjLayer,
            EditorL = Convert.ToInt16(Layer),
            Scale = Object.transform.localScale.x + 0.11f,
            
        });
        
    }

    public void SaveLevel()
    {
        SaveEventPanel.SetActive(true);
        STM.SetState(0);
        try
        {
            for (short i = 0; i < 1000; i++)
            {
                level.Colors[i] = new GeometryDashAPI.Levels.Color(i, GdColors[i].r, GdColors[i].g, GdColors[i].b);
                level.Colors[i].Opacity = GdColors[i].a / 255.0f;
            }
            Invoke("SaveLevelPart2", 1.0f);
        }
        catch
        {
            STM.SetState(2);
            Invoke("SaveMClose", 1.0f);
        }
        
    }

    

    public void SaveLevelPart2()
    {
        try
        {
            local.GetLevelByName(Levelname).LevelString = level.ToString();
            local.Save();
            STM.SetState(1);
            Invoke("SaveMClose", 1.0f);
        }
        catch
        {
            STM.SetState(2);
            Invoke("SaveMClose", 1.0f);
        }
    }

    public void SettingsEvent()
    {
        SettingsWindow.SetActive(true);
        Color32 BGCOLOR = BGrender.color;
        SettingsWindow.GetComponent<SettingsWindow>().EnterAspect(CodeUtil.FloatToByte(BGCOLOR.g));
    }

    void SaveMClose()
    {
        SaveEventPanel.SetActive(false);
    }



}

