using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MapList", menuName = "Scriptable Objects/MapList")]
public class MapList : ScriptableObject
{
    [SerializeField] List<MapData> _mapList = new List<MapData>();

    public List<MapData> GetMapSOList()
    {
        return _mapList;
    }
}
