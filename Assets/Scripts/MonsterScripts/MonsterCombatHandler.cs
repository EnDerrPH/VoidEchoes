using UnityEngine;

public class MonsterCombatHandler : CombatManager
{
    [SerializeField] MonsterController _monsterController;
    GameSceneHandler _gameSceneHandler;
    ParticleSystem _lootDrop;

    public override void Start()
    {
        base.Start();
        _lootDrop = _gameManager.GetVFXData().LootDropVFX;
    }

    public override void AfterDeath()
    {
        base.AfterDeath();
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<BoxCollider>().enabled = false;
        SetLootDropVFX();
    }

    public void SetGameSceneHandler(GameSceneHandler gameSceneHandler)
    {
        _gameSceneHandler = gameSceneHandler;
    }

    private void SetLootDropVFX()
    {
        ParticleSystem lootDropVFX = Instantiate(_lootDrop , transform.position, transform.rotation);
        lootDropVFX.Play();
        _gameSceneHandler.LootDropList.Add(lootDropVFX);
    }

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
