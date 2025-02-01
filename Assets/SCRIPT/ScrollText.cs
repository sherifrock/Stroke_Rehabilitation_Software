using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{
    public float scrollSpeed = 50f; // Speed at which the text will scroll
    private RectTransform rectTransform;
    private Text textComponent;
    private float textWidth;
    private Vector2 initialPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textComponent = GetComponent<Text>();

        // Calculate the width of the text
        textWidth = textComponent.preferredWidth;

        // Store the initial position of the text
        initialPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Move the text to the left over time
        rectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // If the text has completely moved out of view, reset its position
        if (rectTransform.anchoredPosition.x <= -textWidth)
        {
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}
