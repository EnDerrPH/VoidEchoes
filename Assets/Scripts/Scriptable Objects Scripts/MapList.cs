using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MapList", menuName = "Scriptable Objects/MapList")]
public class MapList : ScriptableObject
{
    [SerializeField] List<MapsSO> _mapList = new List<MapsSO>();

    public List<MapsSO> GetMapSOList()
    {
        return _mapList;
    }
}
