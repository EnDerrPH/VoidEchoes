using UnityEngine;

public class CameraMiniMapHandler : MonoBehaviour
{
    [SerializeField] float _xMinClamp;
    [SerializeField] float _xMaxClamp;
    [SerializeField] float _zMinClamp;
    [SerializeField] float _zMaxClamp;
    [SerializeField] Transform _player;
    [SerializeField]  float _xOffset = 120f;
    [SerializeField]  float _zOffset = 250f;
    void Start()
    {
 
    }

    void Update()
    {
        // Apply clamping to the target position to keep the camera within limits
        float clampedX = Mathf.Clamp(_player.position.x, _xMinClamp, _xMaxClamp);
        float clampedZ = Mathf.Clamp(_player.position.z, _zMinClamp, _zMaxClamp);

        // Set the camera's position to the clamped position
        transform.position = new Vector3(clampedX + _xOffset , transform.position.y, clampedZ );
    }

}
