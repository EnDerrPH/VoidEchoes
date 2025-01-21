using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UIGameHandler : UIBaseScript
{
    [Header("GameObjects")]
    [SerializeField] GameObject _miniMap;
    [SerializeField] GameObject _map;
    [SerializeField] GameObject _lineGameObject;
    [SerializeField] GameObject _hpTextGameObject;
    [Header("Sprites")]
    [SerializeField] Sprite _shipIdleSprite;
    [SerializeField] Sprite _shipMovingSprite;
    [SerializeField] Sprite _shipBoarderActivateSprite;
    [SerializeField] Sprite _shipBorderDeactivateSprite;
    [Header("Scripts")]
    [SerializeField] PlayerWeapon _playerWeapon;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] Player _player;
    [SerializeField] Button _shipButton;
    [SerializeField] Image _shipBorder;
    [SerializeField] Image _shipMask;
    [SerializeField] Image _shipImage;
    [SerializeField] List<GameObject> _healthGameObjectUIList = new List<GameObject>();
    UtilitiesSO _utilities;
    string _hexMaxHealth = "#AEFF81";
    string _hexHalfHealth = "#FFD781";
    string _hexNearDeathHealth = "#E04142";
    Color _maxHealthColor;
    Color _halfHealthColor;
    Color _nearDeathColor;
    int _playerMaxHP;
    bool _isShipActivate;
    bool _canUI;

    public override void Start()
    {
        SetPlayer();
        SetScripts();
        base.Start();
        SetShipColors();
        SetHealthUIList();
    }

    public void OnMap(InputValue value)
    {
        _miniMap.SetActive(!_miniMap.activeSelf);
        _map.SetActive(!_map.activeSelf);
    }

    private void OnShipButton()
    {
        _isShipActivate = !_isShipActivate;
        ShowHPUI(_isShipActivate);
        _shipBorder.sprite = _isShipActivate? _shipBoarderActivateSprite : _shipBorderDeactivateSprite;
    }

    private void SetShipHealthColor()
    {
        float halfHP = ((float)_playerMaxHP) * .5f;
        float dangerHP = ((float)_playerMaxHP) * .2f;
        float currentHP = _player.HitPoints;
        float percentage = (currentHP / _playerMaxHP) * 100;
        int roundedPercentage = (int)Math.Round(percentage);
        TMPro.TextMeshProUGUI hpText = _hpTextGameObject.GetComponent<TMPro.TextMeshProUGUI>();

        if(roundedPercentage > -.1)
        {
            hpText.text = "Health "+ roundedPercentage.ToString() + "%";
        }
        Color result = currentHP switch
        {
            var hp when hp > halfHP => _maxHealthColor,  
            var hp when hp <= halfHP && hp > dangerHP => _halfHealthColor,
            var hp when hp <= dangerHP => _nearDeathColor, 
            _ => _maxHealthColor
        };
        _shipImage.color = result;
    }

    private void SetHealthUIList()
    {
        _healthGameObjectUIList.Add(_lineGameObject);
        _healthGameObjectUIList.Add(_hpTextGameObject);
    }

    private void SetPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerWeapon = player.GetComponent<PlayerWeapon>();
        _playerMovement = player.GetComponent<PlayerMovement>();
        _player = player.GetComponent<Player>();
        _playerMaxHP = _player.HitPoints;
    }

    private void SetScripts()
    {
        _utilities = GameManager.instance.GetUtilities();
    }

    private void SetShipColors()
    {
        ColorUtility.TryParseHtmlString(_hexMaxHealth, out _maxHealthColor);
        ColorUtility.TryParseHtmlString(_hexHalfHealth, out _halfHealthColor);
        ColorUtility.TryParseHtmlString(_hexNearDeathHealth, out _nearDeathColor);
    }

 
    private void ShowHPUI(bool IsActive)
    {
        _utilities.SetObjectVisibility(_healthGameObjectUIList, IsActive);
    }


    private void CancelActivatedUI()
    {
        _shipBorder.sprite = _shipBorderDeactivateSprite;
        ShowHPUI(false);
        _isShipActivate = false;
    }

    private void ToggleShipImage()
    {
        _shipMask.sprite = _shipMask.sprite == _shipIdleSprite? _shipMovingSprite : _shipIdleSprite;
    }

    public override void AddListener()
    {
        _shipButton.onClick.AddListener(OnShipButton);
        _playerMovement.OnMoveForwardEvent.AddListener(() => { CancelActivatedUI(); _utilities.ToggleButton(_shipButton);});
        _playerMovement.OnMoveForwardEvent.AddListener(ToggleShipImage);
        _playerMovement.OnTurnEvent.AddListener(ToggleShipImage);
        _player.OnHitEvent.AddListener(SetShipHealthColor);
    }
}
