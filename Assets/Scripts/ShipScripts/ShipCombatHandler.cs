using UnityEngine;
using System;

public class ShipCombatHandler : CombatManager
{
    [SerializeField] GameObject []_desthShipVFX;
    [SerializeField] ParticleSystem []_onHitVFX;
    AudioClip _onDeathSFX;
    Rigidbody _rb;
    bool _hasInitialized;
    public static event Action ShipDeathEvent;
    public override void OnEnable()
    {
        ShipMovementHandler.ShipInitializedEvent += RemoveRigidbodyConstraints;
    }

    public override void OnDisable()
    {
        ShipMovementHandler.ShipInitializedEvent -= RemoveRigidbodyConstraints;
    }

    public override void AfterDeath()
    {
        _shipState = ShipState.Death;
        ShipDeathEvent?.Invoke();
        GetComponent<MeshRenderer>().enabled = false;
        _audioManager.PlayOneShot(_onDeathSFX, _audioVFX, 1f , 1f);
        foreach(GameObject vfx in _desthShipVFX)
        {
            vfx.SetActive(true);
        }
    }
    public void SetDeathSFX(AudioClip DeathSFX)
    {
        _onDeathSFX = DeathSFX;
    }
    private void RemoveRigidbodyConstraints()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.None;
        _hasInitialized = true;
    }

    private void OnHitVFX()
    {
        int randomIndex = UnityEngine.Random.Range(0, _onHitVFX.Length);
        ParticleSystem selectedVFX = _onHitVFX[randomIndex];
        if (!selectedVFX.isPlaying)
        {
            selectedVFX.Play();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if(_shipState == ShipState.Death)
        {
            return;
        }
        MonsterProjectileHandler projectileHandler = other.GetComponent<MonsterProjectileHandler>();
        if(projectileHandler != null && projectileHandler.ParticleType == ParticleType.Monster)
        {
            OnHitVFX();
            CameraShake.Instance.ShakeCamera();
            OnHit(projectileHandler.Damage); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!_hasInitialized || _shipState == ShipState.Death)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Terrain"))
        {
           AfterDeath();
        }
    }
}
