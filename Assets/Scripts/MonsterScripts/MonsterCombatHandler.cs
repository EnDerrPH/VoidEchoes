using UnityEngine;

public class MonsterCombatHandler : CombatManager
{
    [SerializeField] MonsterController _monsterController;

    private void OnParticleCollision(GameObject other)
    {
        if(_health <= 0)
        {
            return;
        }
        if (other.GetComponent<WeaponManager>().ParticleType == ParticleType.Player)
        {
            OnHit(other.GetComponent<WeaponManager>().Damage); 
        }
    }
}
