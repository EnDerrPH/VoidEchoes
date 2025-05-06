using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectionButtonHandler : InitializeManager , IPointerClickHandler
{
    [SerializeField] Image _border;
    [SerializeField] Image _characterImage;
    [SerializeField] UICharacterSceneHandler _UICharacterSceneHandler;
    [SerializeField] CharacterSceneHandler _characterSceneHandler;
    CharacterData _characterData;
    int _selectedID;

    public void SetCharacterData(Sprite characterSprite, CharacterData characterData)
    {
        _characterImage.sprite = characterSprite;
        _characterData = characterData;
    }

    public void SetUICharacterSceneHandler(UICharacterSceneHandler UICharacterSceneHandler)
    {
        _UICharacterSceneHandler = UICharacterSceneHandler;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _UICharacterSceneHandler.CharacterNameText.text = _characterData.CharacterName;
        _characterSceneHandler.SetSelectedCharacter(_characterData.CharacterIDNumber);
        _characterSceneHandler.SetSelectedShip(_characterData.CharacterIDNumber);
        _UICharacterSceneHandler.MoveBorder(_border);
        _audioManager.PlaySound(_audioManager.GetAudioClipData().MainMenuButtonSFX , .6f);
        _UICharacterSceneHandler.SetCharacterData(_characterData);
    }

    public void SetCharacterSceneHandler(CharacterSceneHandler characterSceneHandler)
    {
        _characterSceneHandler = characterSceneHandler;
    }

    public void SetCharacterData(CharacterData characterData)
    {
        _characterData = characterData;
    }
}
