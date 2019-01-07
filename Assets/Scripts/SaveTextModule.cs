using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTextModule : MonoBehaviour
{
    public Image SavingLogo;
    Text ModuleText;
    void Start()
    {
        
    }

    public void SetState(int id)
    {
        if (ModuleText == null)
        {
            ModuleText = GetComponent<Text>();
        }
        switch (id)
        {
            case 0:
                ModuleText.text = "Saving..";
                SavingLogo.gameObject.SetActive(true);
                break;
            case 1:
                ModuleText.text = "Successfully";
                SavingLogo.gameObject.SetActive(false);
                break;
            case 2:
                ModuleText.text = "Error";
                SavingLogo.gameObject.SetActive(false);
                break;
        }
    }
}
