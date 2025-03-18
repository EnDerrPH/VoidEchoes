using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] Transform []_monsterPos;
    public Transform StartingPosition {get => _startPos;}
    public Transform EndPosition {get => _endPos;}
    public Transform []MonsterPos {get => _monsterPos;}
}
