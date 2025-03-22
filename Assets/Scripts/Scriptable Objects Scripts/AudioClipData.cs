using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipData", menuName = "Scriptable Objects/AudioClipData")]
public class AudioClipData : ScriptableObject
{
    [Header ("UI SFX")]
    [SerializeField] AudioClip _mainMenuButtonSFX;
    [SerializeField] AudioClip _groundStepSFX;
    [SerializeField] AudioClip _stairStepSFX;
    [SerializeField] AudioClip _hyperDriveSFX;
    [Header ("BGM SFX")]
    [SerializeField] AudioClip _mainMenuBGM;
    [SerializeField] AudioClip _homeBGM;
    [SerializeField] AudioClip _characterSelectionBGM;
    [SerializeField] AudioClip _gameBGM;
    [Header ("Ship SFX")]
    [SerializeField] AudioClip _onAttackModeSFX;
    [SerializeField] AudioClip _onFireSFX;
    [SerializeField] AudioClip _onMovemeFowardSFX;
    [SerializeField] AudioClip _onStopMovementSFX;
    [SerializeField] AudioClip _onDeathSFX;
    [SerializeField] AudioClip _shipOnHitSFX;
    [Header ("Ship SFX")]
    [SerializeField] AudioClip _lootNormalSFX;
    [SerializeField] AudioClip _lootRareSFX;
    public AudioClip MainMenuButtonSFX {get => _mainMenuButtonSFX;}
    public AudioClip OnAttackModeSFX {get => _onAttackModeSFX;}
    public AudioClip OnMoveForwardSFX {get => _onMovemeFowardSFX;}
    public AudioClip OnStopMovementSFX {get => _onStopMovementSFX;}
    public AudioClip OnDeathSFX {get => _onDeathSFX;}
    public AudioClip OnFireSFX {get => _onFireSFX;}
    public AudioClip GroundStepSFX {get => _groundStepSFX;}
    public AudioClip StairStepSFX {get => _stairStepSFX;} 
    public AudioClip MainMenuBGM {get => _mainMenuBGM;}
    public AudioClip CharacterSelectionBGM {get => _characterSelectionBGM;}
    public AudioClip HomeBGM {get => _homeBGM;}
    public AudioClip GameBGM {get => _gameBGM;}
    public AudioClip HyperDriveSFX {get => _hyperDriveSFX;}
    public AudioClip LootNormalSFX {get => _lootNormalSFX;}
    public AudioClip LootRareSFX {get => _lootRareSFX;}
    public AudioClip ShipoOnHitSFX {get => _shipOnHitSFX;}
}
