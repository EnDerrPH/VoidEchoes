using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] protected ShipState _shipState;
    [SerializeField] protected AudioSource _audioVFX;
    [SerializeField] protected AudioSource _audioSpeech;
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
        _audioManager = _gameManager.GetAudioManager();
        _audioVFX = transform.Find("AudioVFX")?.GetComponent<AudioSource>();
        _audioSpeech = transform.Find("AudioSpeech")?.GetComponent<AudioSource>();
    }
}
