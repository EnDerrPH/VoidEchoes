using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterDataList", menuName = "Scriptable Objects/CharacterDataList")]
public class CharacterDataList : ScriptableObject
{
    [SerializeField] List<CharacterData> _characterDataList = new List<CharacterData>();

    public List<CharacterData> GetCharacterList()
    {
        return _characterDataList;
    }
}
