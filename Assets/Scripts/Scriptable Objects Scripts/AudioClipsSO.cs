using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips", menuName = "Scriptable Objects/AudioClips")]
public class AudioClipsSO : ScriptableObject
{
    [SerializeField] AudioClip _mainMenuButtonSFX;
    [SerializeField] AudioClip _groundStepSFX;
    [SerializeField] AudioClip _stairStepSFX;
    [SerializeField] AudioClip _mainMenuBGM;
    [SerializeField] AudioClip _homeBGM;
    [SerializeField] AudioClip _mapBGM;
    [SerializeField] AudioClip _hyperDriveSFX;

    public AudioClip MainMenuButtonSFX {get => _mainMenuButtonSFX; set => _mainMenuButtonSFX = value;}
    public AudioClip GroundStepSFX {get => _groundStepSFX; set => _groundStepSFX = value;}
    public AudioClip StairStepSFX {get => _stairStepSFX; set => _stairStepSFX = value;}
    public AudioClip MainMenuBGM {get => _mainMenuBGM; set => _mainMenuBGM = value;}
    public AudioClip HomeBGM {get => _homeBGM; set => _homeBGM = value;}
    public AudioClip MapBGM {get => _mapBGM; set => _mapBGM = value;}
    public AudioClip HyperDriveSFX {get => _hyperDriveSFX; set => _hyperDriveSFX = value;}
}
