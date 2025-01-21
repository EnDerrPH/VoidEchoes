using UnityEngine;

public class ParticleDamageHandler : MonoBehaviour
{
   [SerializeField] int _damage;
   [SerializeField] ParticleType _particleType;

   public ParticleType ParticleType => _particleType;
   public int Damage => _damage;
}
