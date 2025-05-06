using UnityEngine;

[CreateAssetMenu(fileName = "VFXData", menuName = "Scriptable Objects/VFXData")]
public class VFXData : ScriptableObject
{
    [SerializeField] ParticleSystem _explosion01VFX;
    [SerializeField] ParticleSystem _explosion02VFX;
    [SerializeField] ParticleSystem _shipOnHitVFX;
    [SerializeField] ParticleSystem _shipAmmoVFX;
    [SerializeField] ParticleSystem _lootDropVFX;
    [SerializeField] ParticleSystem _lootProjectileVFX;

    public ParticleSystem Explosion01VFX {get => _explosion01VFX ;}
    public ParticleSystem Explosion02VFX {get => _explosion02VFX ;}
    public ParticleSystem ShipOnHitVFX {get => _shipOnHitVFX ;}
    public ParticleSystem ShipAmmoVFX {get => _shipAmmoVFX ;}
    public ParticleSystem LootDropVFX {get => _lootDropVFX ;}
    public ParticleSystem LootDropProjectileVFX {get => _lootProjectileVFX ;}
}
