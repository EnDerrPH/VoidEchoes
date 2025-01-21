using UnityEngine;

public class Player : Entity
{
    float _immuneToTrapTimer;
    float _ImmuntToTrapTimerLimit = 2.5f;
    [SerializeField] bool _isImmuneToTrap;

    void Update()
    {
        TripImmunity();
    }

    private void TripImmunity()
    {
        if(!_isImmuneToTrap)
        {
            return;
        }

        _immuneToTrapTimer += Time.deltaTime;

        if(_immuneToTrapTimer >= _ImmuntToTrapTimerLimit)
        {
            _isImmuneToTrap = false;
            _immuneToTrapTimer = 0f;
        }
    }

    public override void OnParticleCollision(GameObject other)
    {    
        if(other.GetComponent<ParticleDamageHandler>() == null)
        {
            return;
        }

        if(other.GetComponent<ParticleDamageHandler>().ParticleType == ParticleType.Ammo)
        {
            OnHit(other.GetComponent<ParticleDamageHandler>());
            return;
        }

        if(other.GetComponent<ParticleDamageHandler>().ParticleType == ParticleType.Trap && !_isImmuneToTrap)
        {
            OnHit(other.GetComponent<ParticleDamageHandler>());
            _isImmuneToTrap = true;
            return;
        }
    }
}
