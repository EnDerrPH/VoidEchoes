using UnityEngine;

public class StepHandler : MonoBehaviour
{
    [SerializeField] float _stepHeight;
    const float _stepOffset =  2f;
    public float StepHeight {get => _stepHeight;}

    void Start()
    {
        _stepHeight = transform.position.y + _stepOffset;
    }
}
