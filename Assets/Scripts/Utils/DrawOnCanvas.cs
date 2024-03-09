using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawOnCanvas : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Texture2D brushTexture; // The brush texture (a simple circle)
    public float brushSize = 10f; // The size of the brush
    public Color brushColor = Color.black; // The color of the brush

    private RectTransform canvasRectTransform;
    private Image canvasImage;
    private Texture2D canvasTexture;
    private Vector2 previousMousePosition;

    private bool isDrawing = false;

    void Start()
    {
        canvasRectTransform = GetComponent<RectTransform>();
        canvasImage = GetComponent<Image>();

        // Create the canvas texture
        canvasTexture = new Texture2D((int)canvasRectTransform.rect.width, (int)canvasRectTransform.rect.height);
        canvasImage.sprite = Sprite.Create(canvasTexture, new Rect(0, 0, canvasRectTransform.rect.width, canvasRectTransform.rect.height), Vector2.zero);
        ClearCanvas();
    }

    void Update()
    {
        // Check for touch/mouse input to start drawing
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        // Check for touch/mouse input to stop drawing
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartDrawing();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrawing)
        {
            Vector2 localPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPosition))
            {
                Draw(localPosition);
            }
        }
    }

    private void StartDrawing()
    {
        isDrawing = true;
        previousMousePosition = Input.mousePosition;
    }

    private void StopDrawing()
    {
        isDrawing = false;
    }

    private void Draw(Vector2 position)
    {
        // Calculate the position in texture coordinates
        //Vector2Int canvasSize = canvasTexture.width * canvasTexture.height;
        Vector2Int brushSizeInt = new Vector2Int((int)brushSize, (int)brushSize);
        Vector2Int drawPosition = new Vector2Int((int)position.x, (int)position.y);

        // Paint on the canvas texture
        for (int x = drawPosition.x - brushSizeInt.x / 2; x < drawPosition.x + brushSizeInt.x / 2; x++)
        {
            for (int y = drawPosition.y - brushSizeInt.y / 2; y < drawPosition.y + brushSizeInt.y / 2; y++)
            {
                if (x >= 0 && x < canvasTexture.width && y >= 0 && y < canvasTexture.height)
                {
                    Color currentColor = canvasTexture.GetPixel(x, y);
                    Color blendedColor = Color.Lerp(currentColor, brushColor, Time.deltaTime * 5); // Smooth blending
                    blendedColor.a = 1f;
                    canvasTexture.SetPixel(x, y, blendedColor);
                }
            }
        }

        canvasTexture.Apply(); // Apply the changes to the texture
    }

    public void ClearCanvas()
    {
        // Clear the canvas texture
        Color[] colors = new Color[canvasTexture.width * canvasTexture.height];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.clear;
        }
        canvasTexture.SetPixels(colors);
        canvasTexture.Apply();
    }
}
