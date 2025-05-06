using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField] Transform _startPos;
    [SerializeField] Transform []_monsterPos;
    public Transform StartingPosition {get => _startPos;}
    public Transform []MonsterPos {get => _monsterPos;}
}
