using UnityEngine;

public class StairsChecker : MonoBehaviour
{
    [SerializeField] CharacterMovement _characterMovement;
    [SerializeField] float _timer;
    bool _isSet;
    void Update()
    {
        if(_isSet)
        {
            _timer += Time.deltaTime;

            if(_timer >= .1f)
            {
                _characterMovement.Movement = new Vector2(0f,0f);
                _timer = 0f;
                _isSet = false;
            }
        }

    }
    void OnTriggerExit(Collider  collision)
    {
        _characterMovement.IsFacingSairs = false;
        _isSet = true;
    }
}
