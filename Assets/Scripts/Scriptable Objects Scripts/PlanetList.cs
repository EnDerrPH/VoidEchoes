using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlanetList", menuName = "Scriptable Objects/PlanetList")]
public class PlanetList : ScriptableObject
{
    [SerializeField] List<Planets> _planetList = new List<Planets>();

    public List<Planets> GetPlanetList()
    {
        return _planetList;
    }
}
