using UnityEngine;

[CreateAssetMenu(fileName = "Planets", menuName = "Scriptable Objects/Planets")]
public class Planets : ScriptableObject
{
    [SerializeField] GameObject _planet;
}
