using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] protected ShipState _shipState;
    protected GameManager _gameManager;
    protected AudioManager _audioManager;
    public ShipState ShipState {get => _shipState; set => _shipState = value;}

    public virtual void OnEnable()
    {
        ShipCombatHandler.ShipDeathEvent += SetDeathState;
    }

    public virtual void OnDisable()
    {
        ShipCombatHandler.ShipDeathEvent -= SetDeathState;
    }

    public virtual void SetDeathState()
    {
        _shipState = ShipState.Death;
    }

    public virtual void Start()
    {
        _gameManager = GameManager.instance;
        _audioManager = AudioManager.instance;
    }
}
