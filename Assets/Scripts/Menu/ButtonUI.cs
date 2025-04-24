using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    private float originalFontSize;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            originalFontSize = text.fontSize;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
        if (text != null)
        {
            text.fontSize = originalFontSize + 5;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        if (text != null)
        {
            text.fontSize = originalFontSize;
        }
    }
}