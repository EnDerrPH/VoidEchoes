using UnityEngine;

public class ShipBaseScript : MonoBehaviour
{
    [SerializeField] protected ShipState _shipState;
    protected GameManager _gameManager;
    protected AudioManager _audioManager;
    public ShipState ShipState {get => _shipState; set => _shipState = value;}

    public virtual void Start()
    {
        _gameManager = GameManager.instance;
        _audioManager = _gameManager.GetAudioManager();
    }
}
