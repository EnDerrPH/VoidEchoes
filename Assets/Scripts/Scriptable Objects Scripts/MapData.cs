using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/MapData")]
public class MapData : ScriptableObject
{
    [SerializeField] string _mapName;
    [SerializeField] Sprite _mapSprite;
    [SerializeField] Terrain _map;
    [SerializeField] MonsterData _monsterData;

    public string MapName {get => _mapName;}
    public Terrain Map {get => _map;}
    public MonsterData MonsterData {get => _monsterData;}
    public Sprite Sprite {get => _mapSprite;}
}
