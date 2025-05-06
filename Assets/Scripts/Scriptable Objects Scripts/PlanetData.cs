using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Scriptable Objects/PlanetData")]
public class PlanetData : ScriptableObject
{
    [SerializeField] string _planetName;
    [SerializeField] GameObject _planet;
    [SerializeField] List<MapData> _mapDataList = new List<MapData>();

    public GameObject Planet => _planet;
    public List<MapData> MapDataList => _mapDataList;
    public string PlanetName => _planetName;
}
