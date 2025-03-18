using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipData", menuName = "Scriptable Objects/AudioClipData")]
public class AudioClipData : ScriptableObject
{
    [Header ("UI SFX")]
    [SerializeField] AudioClip _mainMenuButtonSFX;
    [SerializeField] AudioClip _groundStepSFX;
    [SerializeField] AudioClip _stairStepSFX;
    [SerializeField] AudioClip _mainMenuBGM;
    [SerializeField] AudioClip _homeBGM;
    [SerializeField] AudioClip _characterSelectionBGM;
    [SerializeField] AudioClip _gameBGM;
    [SerializeField] AudioClip _hyperDriveSFX;
    [Header ("Ship SFX")]
    [SerializeField] AudioClip _OnAttackModeSFX;
    [SerializeField] AudioClip _onFireSFX;
    [SerializeField] AudioClip _OnMovemeFowardSFX;
    [SerializeField] AudioClip _OnStopMovementSFX;
    public AudioClip MainMenuButtonSFX {get => _mainMenuButtonSFX;}
    public AudioClip OnAttackModeSFX {get => _OnAttackModeSFX;}
    public AudioClip OnMoveForwardSFX {get => _OnMovemeFowardSFX;}
    public AudioClip OnStopMovementSFX {get => _OnStopMovementSFX;}
    public AudioClip OnFireSFX {get => _onFireSFX;}
    public AudioClip GroundStepSFX {get => _groundStepSFX;}
    public AudioClip StairStepSFX {get => _stairStepSFX;} 
    public AudioClip MainMenuBGM {get => _mainMenuBGM;}
    public AudioClip CharacterSelectionBGM {get => _characterSelectionBGM;}
    public AudioClip HomeBGM {get => _homeBGM;}
    public AudioClip GameBGM {get => _gameBGM;}
    public AudioClip HyperDriveSFX {get => _hyperDriveSFX;}
}
