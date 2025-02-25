using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips", menuName = "Scriptable Objects/AudioClips")]
public class AudioClipsSO : ScriptableObject
{
    [SerializeField] AudioClip _mainMenuButtonSFX;
    [SerializeField] AudioClip _groundStepSFX;
    [SerializeField] AudioClip _stairStepSFX;
    [SerializeField] AudioClip _mainMenuBGM;
    [SerializeField] AudioClip _homeBGM;
    [SerializeField] AudioClip _characterSelectionBGM;
    [SerializeField] AudioClip _mapBGM;
    [SerializeField] AudioClip _hyperDriveSFX;

    public AudioClip MainMenuButtonSFX {get => _mainMenuButtonSFX;}
    public AudioClip GroundStepSFX {get => _groundStepSFX;}
    public AudioClip StairStepSFX {get => _stairStepSFX;} 
    public AudioClip MainMenuBGM {get => _mainMenuBGM;}
    public AudioClip CharacterSelectionBGM {get => _characterSelectionBGM;}
    public AudioClip HomeBGM {get => _homeBGM;}
    public AudioClip MapBGM {get => _mapBGM;}
    public AudioClip HyperDriveSFX {get => _hyperDriveSFX;}
}
