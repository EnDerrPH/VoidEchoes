using UnityEngine;
using UnityEngine.Events;

public class BaseCombatHandler : MonoBehaviour
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

    private void OnDeath()
    {
        if(_health <= 0)
        {
            OnDeathEvent?.Invoke();
        }
    }
}
