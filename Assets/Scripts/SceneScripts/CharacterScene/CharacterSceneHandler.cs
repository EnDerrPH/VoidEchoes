using System.Collections.Generic;
using UnityEngine;

public class CharacterSceneHandler : InitializeManager
{
    [SerializeField] Transform _shipParent;
    [SerializeField] Transform _characterParent;
    [SerializeField] Transform _characterTransform;
    [SerializeField] Transform _shipTransform;
    List<CharacterData> _characterList = new List<CharacterData>();
    Dictionary<int , GameObject> _characterDictionary = new Dictionary<int, GameObject>();
    Dictionary<int , GameObject> _shipDictionary = new Dictionary<int, GameObject>();
    GameObject _currentCharacter;
    GameObject _currentShip;

    public override void InitializeComponents()
    {
        base.InitializeComponents();
        _characterList = _gameManager.GetCharacterDataList().GetCharacterList();
        SetCharacters();
    }
    
    private void SetCharacters()
    {
       // List<CharacterData> _characterList = _gameManager.GetCharacterDataList().GetCharacterList();
        if(_characterParent.childCount == _characterList.Count)
        {
            return;
        }

        foreach(CharacterData characterData in _characterList)
        {

            GameObject shipObj = Instantiate(characterData.Ship, _shipTransform.position, _shipTransform.rotation, _shipParent);
            shipObj.SetActive(false);
            GameObject characterObj = Instantiate(characterData.Character, _characterTransform.position, _characterTransform.rotation, _characterParent);
            characterObj.SetActive(false);

            _characterDictionary.Add(characterData.CharacterIDNumber, characterObj);
            _shipDictionary.Add(characterData.CharacterIDNumber, shipObj);
        }

        SetSelectedCharacter(0);
        SetSelectedShip(0);
    }

    public void SetSelectedCharacter(int ID)
    {
        if(_characterDictionary.TryGetValue(ID, out GameObject character))
        {
            if(_currentCharacter != null)
            {
                _currentCharacter.SetActive(false);
            }
            character.SetActive(true);
            _currentCharacter = character;
        }
    }

    public void SetSelectedShip(int ID)
    {
        if(_shipDictionary.TryGetValue(ID, out GameObject ship))
        {
            if(_currentShip != null)
            {
                _currentShip.SetActive(false);
            }
            ship.SetActive(true);
            _currentShip = ship;
        }
    }
}
