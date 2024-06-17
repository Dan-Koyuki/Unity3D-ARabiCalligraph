using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ARScanDraw : MonoBehaviour
{
    WebCamTexture myCam;
    public RawImage MyCamera;
    private Texture2D rotatedTexture;
    public Image S1K1;
    public Image S1K2;
    public Image S1K3;
    public Image S1K4;
    public Image S1K5;

    void Start()
    {
        // Hide all images first
        S1K1.gameObject.SetActive(false);
        S1K2.gameObject.SetActive(false);
        S1K3.gameObject.SetActive(false);
        S1K4.gameObject.SetActive(false);
        S1K5.gameObject.SetActive(false);
        
        myCam = new WebCamTexture();

        // Set a higher resolution if available
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            myCam.deviceName = devices[0].name;
            myCam.requestedWidth = 1280;
            myCam.requestedHeight = 720;
            myCam.requestedFPS = 30;
        }

        MyCamera.texture = myCam;
        myCam.Play();

        string mychoice = PlayerPrefs.GetString("MyChoice");
        if (!string.IsNullOrEmpty(mychoice))
        {
            ShowSelectedImage(mychoice);
        }
    }

    void Update()
    {
        // Check if the camera has updated its texture
        if (myCam.didUpdateThisFrame)
        {
            RotateWebCamTexture();
        }
    }

    void RotateWebCamTexture()
    {
        int width = myCam.width;
        int height = myCam.height;

        if (rotatedTexture == null || rotatedTexture.width != height || rotatedTexture.height != width)
        {
            rotatedTexture = new Texture2D(height, width);
            MyCamera.texture = rotatedTexture;
        }

        Color32[] pixels = myCam.GetPixels32();
        Color32[] rotatedPixels = new Color32[pixels.Length];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                rotatedPixels[x * height + (height - y - 1)] = pixels[y * width + x];
            }
        }

        rotatedTexture.SetPixels32(rotatedPixels);
        rotatedTexture.Apply();

        // Adjust the RawImage's rectTransform to match the correct orientation
        MyCamera.rectTransform.localEulerAngles = new Vector3(0, 0, 180); // Adjust rotation for correct orientation
        MyCamera.rectTransform.sizeDelta = new Vector2(height, width);   // Swap width and height
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void ShowSelectedImage(string choice)
    {
        // Show the selected image based on the choice
        switch (choice)
        {
            case "S1K1":
                S1K1.gameObject.SetActive(true);
                break;
            case "S1K2":
                S1K2.gameObject.SetActive(true);
                break;
            case "S1K3":
                S1K3.gameObject.SetActive(true);
                break;
            case "S1K4":
                S1K4.gameObject.SetActive(true);
                break;
            case "S1K5":
                S1K5.gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("Unknown choice: " + choice);
                break;
        }
    }
}
