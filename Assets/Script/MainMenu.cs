using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Canvas Info;

    // Start is called before the first frame update
    void Start()
    {
        // Optional: Add initialization code here if needed
        Info.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Add update code here if needed
    }

    public void StartButton()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void OnExitButtonClick()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.Quit();
        }
        else
        {
            Debug.Log("Exit button clicked, but not on Android. No action taken.");
        }
    }

    public void ToggleInfo(bool value){
        Info.gameObject.SetActive(value);
    }
}
