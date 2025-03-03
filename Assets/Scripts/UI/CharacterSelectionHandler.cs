using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CharacterSelectionHandler : UIBaseScript
{
    [SerializeField] Transform _characterButtonContent;
    [SerializeField] Transform _characterContent;
    [SerializeField] Transform _shipContent;
    [SerializeField] TMP_Text _characterNameText;
    [SerializeField] GameObject _characterPrefab;
    [SerializeField] Button _startGameButton;
    CharacterData _selectedCharacterData;
    float _rotationContentValue = -140f;
    float _characterContentXPos = 8f;
    int _selectedCharacter = 0;
    int _previousCharacter;
    Image _currentBorder;
    Image _previousBorder;
    float _borderOffsetX = 60f;

    public int SelectedCharacter {get => _selectedCharacter; set => _selectedCharacter = value;}
    public TMP_Text CharacterNameText {get => _characterNameText; set => _characterNameText = value;}
    public CharacterData SelectedCharacterData {get => _selectedCharacterData; set => _selectedCharacterData = value;}

    [SerializeField] List<CharacterData> _characterList = new List<CharacterData>();

    public override void Start()
    {
        base.Start();
        SetCharacterList();
        SetCharactersUI();
        SetContentRotation(_shipContent);
        SetContentRotation(_characterContent);
        SetContentPosition(_characterContent ,_characterContentXPos, 0f , 0f);
        SetContentPosition(_shipContent, 0f, 0f , 1f);
        SetSelectedCharacter();
    }

    public override void AddListener()
    {
        base.AddListener();
        _startGameButton.onClick.AddListener(StartGame);
    }

    public void SetSelectedCharacter()
    {
        _characterContent.GetChild(_previousCharacter).gameObject.SetActive(false);
        _shipContent.GetChild(_previousCharacter).gameObject.SetActive(false);
        _characterContent.GetChild(_selectedCharacter).gameObject.SetActive(true);
        _shipContent.GetChild(_selectedCharacter).gameObject.SetActive(true);
        _previousCharacter = _selectedCharacter;
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
    }

    private void SetCharacterList()
    {
        CharacterDataList CharacterDataList = GameManager.instance.GetCharacterDataList();
        _characterList = CharacterDataList.GetCharacterList();
    }

    private void SetCharactersUI()
    {
        if(_characterContent.childCount == _characterList.Count)
        {
            return;
        }

        foreach(CharacterData characterData in _characterList)
        {
            GameObject characterButtonObj = Instantiate(_characterPrefab, _characterButtonContent.position, _characterButtonContent.rotation, _characterButtonContent.transform);
            CharacterSelectionButtonHandler characterUI = characterButtonObj.GetComponent<CharacterSelectionButtonHandler>();
            characterUI.SetCharacterData(characterData.CharacterIcon , characterData);
            GameObject shipObj = Instantiate(characterData.Ship, characterData.Ship.transform.position, characterData.Ship.transform.rotation, _shipContent.transform);
            shipObj.SetActive(false);
            GameObject characterObj = Instantiate(characterData.Character, characterData.Character.transform.position, characterData.Character.transform.rotation, _characterContent.transform);
            characterObj.SetActive(false);
        }
    }

    private void SetContentRotation(Transform contentTransform)
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y = _rotationContentValue;
        contentTransform.transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void SetContentPosition(Transform contentTransform, float xPos, float yPos, float zPos)
    {
        contentTransform.position = new Vector3(xPos, yPos , zPos);
    }

    private void StartGame()
    {
        GameManager.instance.SetCharacterData(_selectedCharacterData);
        _loadingSceneManager.LoadScene("Home");
        GameManager.instance.SetScene(GameScene.Home);
    }
}
