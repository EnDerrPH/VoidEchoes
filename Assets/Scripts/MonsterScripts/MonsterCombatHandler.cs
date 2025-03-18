using UnityEngine;

public class MonsterCombatHandler : BaseCombatHandler
{
    [SerializeField] MonsterController _monsterController;

    private void OnParticleCollision(GameObject other)
    {
        if(_health <= 0)
        {
            return;
        }
        if (other.GetComponent<BaseWeaponScript>().ParticleType == ParticleType.Player)
        {
            OnHit(other.GetComponent<BaseWeaponScript>().Damage); 
        }
    }
}
