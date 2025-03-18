using UnityEngine;
using UnityEngine.UI;
public class UIGameSceneHandler : MonoBehaviour
{
    [SerializeField] Image _statusBorder;
    ShipAttackModeHandler _shipAttackModeHandler;
    string _standbyModeHex = "#FFFFFF";
    string _attackModeHex = "#FF8686";
    bool _isAttackmode;
    Color _standbyModeColor;
    Color _attackModeColor;

    private void Start()
    {
        SetColors();
    }

    private void SetColors()
    {
        ColorUtility.TryParseHtmlString(_standbyModeHex, out _standbyModeColor);
        ColorUtility.TryParseHtmlString(_attackModeHex, out _attackModeColor);
    }

    public void SetShipAttackModeHandler(ShipAttackModeHandler shipAttackModeHandler)
    {
        _shipAttackModeHandler = shipAttackModeHandler;
    }
}
