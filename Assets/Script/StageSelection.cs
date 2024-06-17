using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StageSelection : MonoBehaviour
{
    public Canvas StyleSelection;
    private readonly string[] Code = new string[] { "S1K1", "S1K2", "S1K3", "S1K4", "S1K5", "S2K1", "S2K2", "S2K3", "S2K4", "S2K5", "S3K1", "S3K2", "S3K3", "S3K4", "S3K5" };

    [SerializeField] private GameObject[] CallObjects;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var callObject in CallObjects)
        {
            callObject.SetActive(false);
        }

        ToggleStage("S1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HomeButton(){
        SceneManager.LoadScene("Main Menu");
    }

    public void SelectedStage(string destinationAndNumber)
    {
        // Split the input string to get destination and number
        string[] parts = destinationAndNumber.Split(',');
        if (parts.Length == 2)
        {
            string destination = parts[0];
            if (int.TryParse(parts[1], out int number))
            {
                PlayerPrefs.SetString("MyChoice", Code[number]);
                PlayerPrefs.Save();
                SceneManager.LoadScene(destination);
            }
        }
    }

    public void ToggleStyle(bool value)
    {
        StyleSelection.gameObject.SetActive(value);
    }

    public void ToggleStage(string value)
    {
        string StageState = value;
        UpdateStage(StageState);
        ToggleStyle(false);
    }

    public void UpdateStage(string value)
    {
        // Deactivate all call objects
        foreach (var callObject in CallObjects)
        {
            callObject.SetActive(false);
        }

        // Activate only the relevant call objects based on the stage
        switch (value)
        {
            case "S1":
                ActivateCalls(0, 5);
                break;
            case "S2":
                ActivateCalls(5, 10);
                break;
            case "S3":
                ActivateCalls(10, 15);
                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    }

    private void ActivateCalls(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            CallObjects[i].SetActive(true);
        }
    }
}
