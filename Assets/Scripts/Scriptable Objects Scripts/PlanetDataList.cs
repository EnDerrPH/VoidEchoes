using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlanetDataList", menuName = "Scriptable Objects/PlanetDataList")]
public class PlanetDataList : ScriptableObject
{
    [SerializeField] List<PlanetData> _planetList = new List<PlanetData>();

    public List<PlanetData> GetPlanetList()
    {
        return _planetList;
    }
}
