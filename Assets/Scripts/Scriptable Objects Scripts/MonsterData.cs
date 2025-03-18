using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData")]
public class MonsterData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] GameObject _monster;
    [SerializeField] ParticleSystem _weapon;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _monsterHealth;
    [SerializeField] int _damage;
    [Header("Monster SFX")]
    [SerializeField] AudioClip _onHitSFX;
    [SerializeField] AudioClip _onDeathSFX;
    [SerializeField] AudioClip _onAttackSFX;
    [SerializeField] AudioClip _onMoveSFX;

    public string Name {get => _name;}
    public GameObject Monster {get => _monster;}
    public ParticleSystem Weapon {get => _weapon;}
    public AudioClip OnHitSFX {get => _onHitSFX;}
    public AudioClip OnDeathSFX {get => _onDeathSFX;}
    public AudioClip OnAttackSFX {get => _onAttackSFX;}
    public AudioClip OnMoveSFX {get => _onMoveSFX;}
    public float MoveSpeed {get => _moveSpeed;}
    public int MonsterHealth {get => _monsterHealth;}
    public int Damage {get => _damage;}

}
