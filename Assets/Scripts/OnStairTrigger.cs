using UnityEngine;

public class OnStairTrigger : BaseTriggerManager
{
    void OnTriggerEnter(Collider  collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _characterMovement.IsFacingStairs = true;
        }
    }

    void OnTriggerExit(Collider  collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _characterMovement.IsFacingStairs = false;
        }
    }
}
