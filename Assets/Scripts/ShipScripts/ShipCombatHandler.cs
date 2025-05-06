using UnityEngine;
using System;

public class ShipCombatHandler : CombatManager
{
    ShipState _shipState;
    [SerializeField] ParticleSystem _explosion01VFX;
    [SerializeField] ParticleSystem _explosion02VFX;
    [SerializeField] ParticleSystem _onHitVFX;
    [SerializeField] AudioClip _onDeathSFX;
    bool _hasInitialized;
    public static event Action ShipDeathEvent;
    private void OnEnable()
    {
        ShipMovementHandler.ShipInitializedEvent += RemoveRigidbodyConstraints;
    }

    private void OnDisable()
    {
        ShipMovementHandler.ShipInitializedEvent -= RemoveRigidbodyConstraints;
    }


    public override void AfterDeath()
    {
        _shipState = ShipState.Death;
        ShipDeathEvent?.Invoke();
        GetComponent<MeshRenderer>().enabled = false;
        _audioManager.PlaySound(_onDeathSFX, 1f);
        OnDeathVFX();
    }
    public void SetDeathSFX(AudioClip DeathSFX)
    {
        _onDeathSFX = DeathSFX;
    }


    public void SetVFX(ParticleSystem onHitVFX , ParticleSystem explosion01VFX, ParticleSystem explosion02VFX)
    {
        _onHitVFX = onHitVFX;
        _explosion01VFX = explosion01VFX;
        _explosion02VFX = explosion02VFX;
    }

    private void RemoveRigidbodyConstraints()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _hasInitialized = true;
    }

    private void OnDeathVFX()
    {
        _explosion01VFX.gameObject.transform.position = transform.position;
        _explosion02VFX.gameObject.transform.position = transform.position;
        _explosion01VFX.Play();
        _explosion02VFX.Play();

    }

    private void OnHitVFX()
    {
        if (_onHitVFX == null) return;

        Collider shipCollider = GetComponent<Collider>();
        if (shipCollider == null) return;

        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(shipCollider.bounds.min.x, shipCollider.bounds.max.x),
            UnityEngine.Random.Range(shipCollider.bounds.min.y, shipCollider.bounds.max.y),
            UnityEngine.Random.Range(shipCollider.bounds.min.z, shipCollider.bounds.max.z)
        );

        _onHitVFX.transform.position = randomPosition;
        _onHitVFX.Play();
    }
    private void OnParticleCollision(GameObject other)
    {
        if(_shipState == ShipState.Death)
        {
            return;
        }
        ProjectileHandler projectileHandler = other.GetComponent<ProjectileHandler>();

        if(projectileHandler != null && projectileHandler.ParticleType == ParticleType.Monster)
        {
            OnHitVFX();
            _audioManager.PlaySound(_audioManager.GetAudioClipData().ShipoOnHitSFX, .5f);
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
