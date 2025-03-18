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
    [SerializeField] int _shipHealth;
    [SerializeField] int _shipDamage;
    [Header("CharacterSelection Screen Positions")]
    [SerializeField] float _startingXPosition;
    [SerializeField] float _startingZPosition;
    [Header("AttackMode SFX")]
    [SerializeField] List<AudioClip> _audioAttackList = new List<AudioClip>();

    public int CharacterIDNumber {get => _characterIdNumber;}
    public int ShipHealth {get => _shipHealth;}
    public int ShipDamage {get => _shipDamage;}
    public float StartingXPosition {get => _startingXPosition;}
    public float StartingZPosition {get => _startingZPosition;}
    public string CharacterName {get => _characterName;}
    public GameObject Ship {get => _ship;}
    public GameObject Character {get => _character;}
    public Sprite CharacterIcon {get => _characterIcon;}
    public List<AudioClip> AudioAttackList {get => _audioAttackList;}
}
