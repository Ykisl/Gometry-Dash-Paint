using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsWindow : MonoBehaviour
{
    public Text BGL;
    public SpriteRenderer BGIAMGE;
    public static byte FBGL;
    public Button CloseBTN;
    public Button SaveBTN;
    public Button OkBTN;
    BGLModule bGLModule;
    

    void Start()
    {
        FBGL = CodeUtil.StringToByte(BGL.text);
        bGLModule = GetComponentInChildren<BGLModule>();

        //DEL

        CloseBTN.onClick.AddListener(delegate { CloseEvent(); });
        SaveBTN.onClick.AddListener(delegate { SaveEvent(); });
        OkBTN.onClick.AddListener(delegate { OkEvent(); });
    }

    public void EnterAspect(byte entaspect)
    {
        FBGL = entaspect;
        bGLModule.sl.value = FBGL;
    }

    
    void Update()
    {
        BGIAMGE.color = new Color32(CodeUtil.StringToByte(BGL.text), CodeUtil.StringToByte(BGL.text), CodeUtil.StringToByte(BGL.text), 255);
    }

    void OkEvent()
    {
        FBGL = CodeUtil.StringToByte(BGL.text);
        gameObject.SetActive(false);
    }

    void SaveEvent()
    {
        FBGL = CodeUtil.StringToByte(BGL.text);
    }

    void CloseEvent()
    {
        BGIAMGE.color = new Color32(FBGL, FBGL, FBGL, 255);
        bGLModule.sl.value = FBGL;
        gameObject.SetActive(false);
    }

}
