using UnityEngine;

public class InteractableHandler : MonoBehaviour
{
    [SerializeField] protected bool _isOpen;
    [SerializeField] protected GameObject _canvas;
    [SerializeField] protected InteractableType _interactableType;
    public bool IsOpen {get => _isOpen; set => _isOpen = value;}
    public InteractableType InteractableType { get => _interactableType;}

    public void ActivateObject(bool isActive)
    {
        _isOpen = isActive;
    }

    public void ActivateCanvas(bool isActive)
    {
        if(_isOpen)
        {
            _canvas.SetActive(false);
            return;
        }
        _canvas.SetActive(isActive);
    }
}
