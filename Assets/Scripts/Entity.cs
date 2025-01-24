using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Transform _explosionVFX;
    [SerializeField] protected Transform _onHitVFX;
    [SerializeField] protected int _hitPoints;
    [SerializeField] GameObject _shipModel;
    [SerializeField] GameObject _parentObject;
    [SerializeField] GameObject _minimapMarker;
    public int HitPoints => _hitPoints;
    public UnityEvent OnDeathEvent, OnHitEvent;

    public virtual void ActivateVFX(Transform ParentVFX)
    {
        foreach(Transform child in ParentVFX)
        {
            child.gameObject.SetActive(true);
        }
    }

    public virtual void Death()
    {
        _explosionVFX.transform.position = _shipModel.transform.position;
        ActivateVFX(_explosionVFX);
        OnDeathEvent.Invoke();
    }

    public virtual void OnHit(ParticleDamageHandler particle)
    {
        _hitPoints -= particle.Damage;
        ActivateVFX(_onHitVFX);
        OnHitEvent.Invoke();
        if(_hitPoints <= 0)
        {
            Death();
            StartCoroutine(DeactivateObject(.5f));
        }
    }


    public virtual void OnParticleCollision(GameObject other)
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
    }

    private IEnumerator DeactivateObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        _shipModel.gameObject.SetActive(false);
        _minimapMarker.SetActive(false);
    }


}
