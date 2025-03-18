using UnityEngine;

public class ShipCombatHandler : CombatManager
{
    private void OnParticleCollision(GameObject other)
    {
        MonsterProjectileHandler projectileHandler = other.GetComponent<MonsterProjectileHandler>();
        if(projectileHandler != null && projectileHandler.ParticleType == ParticleType.Monster)
        {
            CameraShake.Instance.ShakeCamera();
            OnHit(projectileHandler.Damage); 
        }
    }
}
