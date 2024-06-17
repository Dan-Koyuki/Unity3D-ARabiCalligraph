using UnityEngine;

public class Drawing : MonoBehaviour
{
    [SerializeField]
    private float minDistance = 0.1f;

    [SerializeField]
    private int lineWidthInPixels = 15;

    private LineRenderer currentLine;
    private Vector3 previousPosition;

    void Start()
    {
        // Ensure this object is on a specific layer
        gameObject.layer = LayerMask.NameToLayer("DrawLayer");
    }

    void Update()
    {
        // Check if there is a touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle touch phases
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartDrawing(touch.position);
                    break;
                case TouchPhase.Moved:
                    ContinueDrawing(touch.position);
                    break;
                case TouchPhase.Ended:
                    FinishDrawing();
                    break;
            }
        }
    }

    void StartDrawing(Vector3 startPosition)
    {
        Debug.Log("Im here");

        // Create a new GameObject with a LineRenderer component
        GameObject lineObj = new GameObject("Line");
        currentLine = lineObj.AddComponent<LineRenderer>();

        // Set the initial width (this will be adjusted dynamically in UpdateLineWidth)
        UpdateLineWidth();

        // Ensure the line renderer is in the correct sorting layer
        currentLine.sortingLayerName = "Foreground"; // or any appropriate layer
        currentLine.sortingOrder = 20; // ensure it renders on top of other elements

        // Initialize the line
        currentLine.positionCount = 1;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(startPosition.x, startPosition.y, Camera.main.nearClipPlane));
        currentLine.SetPosition(0, worldPoint);
        previousPosition = startPosition;
    }

    void ContinueDrawing(Vector3 newPosition)
    {
        Debug.Log("Continue to Im here");
        float distance = Vector3.Distance(previousPosition, newPosition);

        if (distance > minDistance)
        {
            currentLine.positionCount++;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(newPosition.x, newPosition.y, Camera.main.nearClipPlane));
            currentLine.SetPosition(currentLine.positionCount - 1, worldPoint);
            previousPosition = newPosition;
        }
    }

    void FinishDrawing()
    {
        previousPosition = Vector3.zero;
    }

    void UpdateLineWidth()
    {
        if (currentLine != null)
        {
            Debug.Log("Line Exist");
            // Calculate the line width in world units
            float lineWidth = lineWidthInPixels / (float)Screen.dpi * 2.54f / 100f; // Convert pixels to meters
            currentLine.startWidth = lineWidth;
            currentLine.endWidth = lineWidth;
        }
    }
}
