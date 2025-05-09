using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectionButtonHandler : InitializeManager , IPointerClickHandler
{
    [SerializeField] Image _border;
    [SerializeField] Image _characterImage;
    [SerializeField] CharacterSceneHandler _characterSelectionHandler;
    CharacterData _characterData;
    public override void Start()
    {
        base.Start();
        _characterSelectionHandler = GameObject.FindAnyObjectByType<CharacterSceneHandler>();
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
        _audioManager.PlaySound(_audioManager.GetAudioClipData().MainMenuButtonSFX , .6f);
    }
}
