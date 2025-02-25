using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] int _characterIdNumber;
    [SerializeField] string _characterName;
    [SerializeField] GameObject _ship;
    [SerializeField] GameObject _character;
    [SerializeField] Sprite _characterIcon;

    public int CharacterIDNumber {get => _characterIdNumber; set => _characterIdNumber = value;}
    public string CharacterName {get => _characterName; set => _characterName = value;}
    public GameObject Ship {get => _ship; set => _ship = value;}
    public GameObject Character {get => _character; set => _character = value;}
    public Sprite CharacterIcon {get => _characterIcon; set => _characterIcon = value;}
}
