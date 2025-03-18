using UnityEngine;
using UnityEngine.UI;

public class CrosshairHandler : MonoBehaviour
{
    RectTransform _crosshareRect;

    void Start()
    {
        _crosshareRect = GetComponent<RectTransform>();
    }
    void Update()
    {
        FollowMousePointer();
    }

    private void FollowMousePointer()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _crosshareRect.parent as RectTransform, 
            Input.mousePosition, 
            null, 
            out mousePos
        );

        _crosshareRect.anchoredPosition = mousePos;
    }
}
