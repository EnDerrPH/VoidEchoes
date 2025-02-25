using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectionButtonHandler : UIBaseScript , IPointerClickHandler
{
    [SerializeField] Image _border;
    [SerializeField] Image _characterImage;
    [SerializeField] CharacterSelectionHandler _characterSelectionHandler;
    CharacterData _characterData;
    AudioClipsSO _audioClipSO;

    public override void Start()
    {
        base.Start();
        _audioClipSO = GameManager.instance.GetAudioClips();
        _audioSource = GameObject.FindAnyObjectByType<AudioSource>();
        _characterSelectionHandler = GameObject.FindAnyObjectByType<CharacterSelectionHandler>();
    }

    public void SetCharacterData(Sprite characterSprite, CharacterData characterData)
    {
        _characterImage.sprite = characterSprite;
        _characterData = characterData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _characterSelectionHandler.MoveBorder(_border);
        _characterSelectionHandler.SelectedCharacter = _characterData.CharacterIDNumber;
        _characterSelectionHandler.CharacterNameText.text = _characterData.CharacterName;
        _characterSelectionHandler.SelectedCharacterData = _characterData;
        _characterSelectionHandler.SetSelectedCharacter();
        PlayButtonSound(_audioClipSO.MainMenuButtonSFX, _audioSource);
        _audioSource.volume = .1f;
    }
}
