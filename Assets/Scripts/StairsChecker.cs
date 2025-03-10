using UnityEngine;

public class StairsChecker : BaseTriggerManager
{
    [SerializeField] bool _bottomTrigger;

    void OnTriggerEnter(Collider  collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _characterController.IsGoingUp = _bottomTrigger? true : false;
        }
    }
}
