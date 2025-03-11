using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    public Transform StartingPosition {get => _startPos; set => _startPos = value;}
    public Transform EndPosition {get => _endPos; set => _endPos = value;}
}
