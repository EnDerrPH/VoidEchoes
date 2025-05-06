using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] int _characterIdNumber;
    [SerializeField] string _characterName;
    [SerializeField] GameObject _ship;
    [SerializeField] GameObject _character;
    [SerializeField] Sprite _characterIcon;
    [Header("AttackMode SFX")]
    [SerializeField] List<AudioClip> _audioAttackList = new List<AudioClip>();
    [Header("Ship Status")]
    [SerializeField] int _shipHealth;
    [SerializeField] int _shipDamage;
    [SerializeField] int _shipFuel;


    public int CharacterIDNumber {get => _characterIdNumber;}
    public int ShipHealth {get => _shipHealth;}
    public int ShipDamage {get => _shipDamage;}
    public int ShipFuel {get => _shipFuel;}
    public string CharacterName {get => _characterName;}
    public GameObject Ship {get => _ship;}
    public GameObject Character {get => _character;}
    public Sprite CharacterIcon {get => _characterIcon;}
    public List<AudioClip> AudioAttackList {get => _audioAttackList;}
}
