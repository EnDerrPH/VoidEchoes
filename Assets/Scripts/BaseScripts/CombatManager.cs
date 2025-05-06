using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    [SerializeField] protected int _health;
    protected GameManager _gameManager;
    protected AudioManager _audioManager;
    protected Rigidbody _rb;
    public UnityEvent OnDeathEvent, OnHitEvent;
    bool _isDead;
    public int Health {get => _health; set => _health = value;}

    public virtual void Start()
    {
        _gameManager = GameManager.instance;
        _audioManager = AudioManager.instance;
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void OnHit(int damage)
    {
        OnHitEvent?.Invoke();
        _health -= damage;
        OnDeath();
    }

    public void OnDeath()
    {
        if(_health <= 0 && !_isDead)
        {
            _isDead = true;
            AfterDeath();
        }
    }

    public virtual void AfterDeath()
    {
        OnDeathEvent?.Invoke();
    }
}
