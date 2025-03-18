using UnityEngine;

public class BaseWeaponScript : MonoBehaviour
{
   [SerializeField] protected int _damage;
   [SerializeField] protected ParticleType _particleType;

   public ParticleType ParticleType {get => _particleType; set => _particleType = value;}
   public int Damage {get => _damage; set => _damage = value;}
}
