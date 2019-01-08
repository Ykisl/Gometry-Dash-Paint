using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseEventWindow : MonoBehaviour
{
    public LevelEditor le;
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void SaveWindow()
    {
        le.SaveLevel();
        Invoke("OkWindow", 1.5f);
    }

    public void OkWindow()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

}
