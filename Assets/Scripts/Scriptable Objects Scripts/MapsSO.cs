using UnityEngine;

[CreateAssetMenu(fileName = "Maps", menuName = "Scriptable Objects/Maps")]
public class MapsSO : ScriptableObject
{
    [SerializeField] string _mapName;
    [SerializeField] Terrain _map;

    public string MapName {get => _mapName;}
    public Terrain Map {get => _map;}
}
