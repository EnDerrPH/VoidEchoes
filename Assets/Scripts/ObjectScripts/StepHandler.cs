using UnityEngine;

public class StepHandler : ObjectManager
{
    [SerializeField] float _stepHeight;
    const float _stepOffset =  2f;
    public float StepHeight {get => _stepHeight;}

    private void Start()
    {
        _stepHeight = transform.position.y + _stepOffset;
    }
}
