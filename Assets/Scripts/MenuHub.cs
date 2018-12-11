using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeometryDashAPI.Data;
using System;
using UnityEngine.SceneManagement;

public class MenuHub : MonoBehaviour {

    public string exename;
    public string llpath;
    public InputField ExeInput;
    public InputField GroupInput;
    public InputField ZRangeInput;
    public InputField LayerInput;
    public Button LoadLevelBtn;
    public Button NextLevelBTN;
    public Button PrewLevelBTN;
    public Button EditLevelBTN;
    public GameObject NoneLevels;
    public Text LevelsCountlist;
    public Text LevelName;
    public GameObject LevelSheetPanel;
    LocalLevels local;
    int levelscount;
    int thislevel;
    void Start() {
        

        if (!PlayerPrefs.HasKey("GrupVarible"))
        {
            GroupInput.text = "547";
            PlayerPrefs.SetString("GrupVarible", "547");
        }
        else
        {
            GroupInput.text = PlayerPrefs.GetString("GrupVarible");
        }

        if (!PlayerPrefs.HasKey("ZRangeVarible"))
        {
            ZRangeInput.text = "0";
            PlayerPrefs.SetString("ZRangeVarible", "0");
        }
        else
        {
            ZRangeInput.text = PlayerPrefs.GetString("ZRangeVarible");
        }

        if (!PlayerPrefs.HasKey("LayerVarible"))
        {
            LayerInput.text = "24";
            PlayerPrefs.SetString("LayerVarible", "24");
        }
        else
        {
            LayerInput.text = PlayerPrefs.GetString("LayerVarible");
        }

        if (!PlayerPrefs.HasKey("ExeFile"))
        {
            ExeInput.text = "GeometryDash";
            exename = "GeometryDash";
            Invoke("LoadGDLevels", 1.0f);
        }
        else
        {
            ExeInput.text = PlayerPrefs.GetString("ExeFile");
            exename = PlayerPrefs.GetString("ExeFile");
            Invoke("LoadGDLevels", 0.5f);
        }

        ExeInput.onValueChange.AddListener(delegate { ExeInputChange(); });
        LoadLevelBtn.onClick.AddListener(delegate { LoadGDLevels(); });
        NextLevelBTN.onClick.AddListener(delegate { NextLevel(); });
        PrewLevelBTN.onClick.AddListener(delegate { PrewLevel(); });
        GroupInput.onEndEdit.AddListener(delegate { SaveArtGroup(); });
        ZRangeInput.onEndEdit.AddListener(delegate { SaveZRange(); });
        LayerInput.onEndEdit.AddListener(delegate { SaveLayer(); });
        EditLevelBTN.onClick.AddListener(delegate { ClickEditBtn(); });
        levelscount = 0;
        thislevel = 1;
    }


    void Update() {
        if (levelscount == 0)
        {
            LevelSheetPanel.active = false;
            NoneLevels.active = true;
        }
        else
        {
            if(thislevel < 1)
            {
                thislevel = levelscount;
            }
            if (thislevel > levelscount)
            {
                thislevel = 1;
            }
            LevelsCountlist.text = thislevel + "/" + levelscount;
            NoneLevels.active = false;
            LevelName.text = Convert.ToString(local.Levels[thislevel - 1]);
            LevelSheetPanel.active = true;
        }
    }

    void LoadGDLevels()
    {
        llpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + exename + "/CCLocalLevels.dat";
        local = new LocalLevels(llpath);
        levelscount = local.Levels.Count;
        PlayerPrefs.SetString("ExeFile", exename);
    }

    void ClickEditBtn()
    {
        if (levelscount != 0)
        {
            PlayerPrefs.SetInt("edit_level", thislevel - 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Editor", LoadSceneMode.Single);
        }
    }

    public void NextLevel()
    {
        if (levelscount != 0)
        {
            thislevel++;
        }
    }

    void SaveArtGroup()
    {
        PlayerPrefs.SetString("GrupVarible", GroupInput.text);
    }
    void SaveZRange()
    {
        PlayerPrefs.SetString("ZRangeVarible", ZRangeInput.text);
    }
    void SaveLayer()
    {
        PlayerPrefs.SetString("LayerVarible", LayerInput.text);
    }
    public void PrewLevel()
    {
        if (levelscount != 0)
        {
            thislevel--;
        }
    }

    public void SetExeName(string getexename)
    {
        exename = getexename;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }


    public void ExeInputChange()
    {
        SetExeName(ExeInput.text);
    }
}
