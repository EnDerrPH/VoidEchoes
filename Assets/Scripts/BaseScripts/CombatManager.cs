using UnityEngine;
using UnityEngine.Events;

public class CombatManager : ShipManager
{
    [SerializeField] protected int _health;
    public UnityEvent OnDeathEvent, OnHitEvent;


    public int Health {get => _health; set => _health = value;}

    public virtual void OnHit(int damage)
    {
        OnHitEvent?.Invoke();
        _health -= damage;
        OnDeath();
    }

    public void OnDeath()
    {
        if(_health <= 0)
        {
            AfterDeath();
        }
    }

    public virtual void AfterDeath()
    {
        OnDeathEvent?.Invoke();
    }
}
