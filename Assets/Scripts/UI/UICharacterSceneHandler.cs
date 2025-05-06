using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UICharacterSceneHandler : InitializeManager
{
    [SerializeField] GameObject _characterPrefab;
    [SerializeField] Transform _characterButtonContent;
    [SerializeField] TMP_Text _characterNameText;
    [SerializeField] Button _startGameButton;
    [SerializeField] CharacterSceneHandler _characterSceneHandler;
    CharacterData _characterData;
    Image _currentBorder;
    Image _previousBorder;
    float _borderOffsetX = 60f;

    public TMP_Text CharacterNameText {get => _characterNameText; set => _characterNameText = value;}

    public override void InitializeComponents()
    {
        SetCharacterButton();
    }

    public override void AddListener()
    {
        base.AddListener();
        _startGameButton.onClick.AddListener(StartGame);
    }

    public void SetCharacterData(CharacterData characterData)
    {
        _characterData = characterData;
    }

    public void MoveBorder(Image border)
    {
        if(_previousBorder != null)
        {
            _previousBorder.transform.position = new Vector3(_previousBorder.transform.position.x -_borderOffsetX , _previousBorder.transform.position.y ,_previousBorder.transform.position.z);
        }
        _currentBorder = border;
        _currentBorder.transform.position = new Vector3(_currentBorder.transform.position.x + _borderOffsetX, _currentBorder.transform.position.y ,_currentBorder.transform.position.z);
        _previousBorder = _currentBorder;
        _startGameButton.interactable = true;
    }

    private void SetCharacterButton()
    {
        List<CharacterData> characterList = _gameManager.GetCharacterDataList().GetCharacterList();
        if(_characterButtonContent.childCount == characterList.Count)
        {
            return;
        }
        foreach(CharacterData characterData in characterList)
        {
            GameObject characterButtonObj = Instantiate(_characterPrefab, _characterButtonContent.position, _characterButtonContent.rotation, _characterButtonContent.transform);
            CharacterSelectionButtonHandler characterUI = characterButtonObj.GetComponent<CharacterSelectionButtonHandler>();
            characterUI.SetCharacterData(characterData.CharacterIcon , characterData);
            characterUI.SetCharacterSceneHandler(_characterSceneHandler);
            characterUI.SetUICharacterSceneHandler(this);
            characterUI.SetCharacterData(characterData);
        }
    }

    private void StartGame()
    {
        _gameManager.SetCharacterData(_characterData);
        _loadingSceneManager.LoadScene("Home");
    }
}
