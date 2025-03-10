using UnityEngine;

public class InteractableCanvasHandler : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    bool _isInteractable;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}
