using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UtilitiesSO", menuName = "Scriptable Objects/Utilities")]
public class UtilitiesSO : ScriptableObject
{
    public void ToggleButton(Button button)
    {
        button.interactable = !button.interactable;
    }

    public void SetObjectVisibility(List<GameObject> gameObjectList , bool isActive)
    {
        foreach(GameObject gameObject in gameObjectList)
        {
            gameObject.SetActive(isActive);
        }
    }
}


