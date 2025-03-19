using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] protected ObjectType _objectType;
    [SerializeField] protected bool _isOpen;
    [SerializeField] protected GameObject _canvas;
    public bool IsOpen {get => _isOpen; set => _isOpen = value;}
    public ObjectType ObjectType {get => _objectType; set => _objectType = value;}

    public void ActivateObject(bool isActive)
    {
        _isOpen = isActive;
    }

    public void ActivateCanvas(bool isActive)
    {
        if(_canvas == null)
        {
            return;
        }
        if(_isOpen)
        {
            _canvas.SetActive(false);
            return;
        }
        _canvas.SetActive(isActive);
    }
}
