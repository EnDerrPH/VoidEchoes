using UnityEngine;
using TMPro;

public class UIGameSceneHandler : MonoBehaviour
{
    [SerializeField] TMP_Text _fuelText;
    [SerializeField] TMP_Text _shipHPText;



    public void SetFuelText(string fuel)
    {
        _fuelText.text = fuel;
    }

    public void SetHpText(string shipHP)
    {
        _shipHPText.text = shipHP;
    }
}
