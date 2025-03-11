using UnityEngine;

public class OnStairTrigger : MonoBehaviour
{

    void OnTriggerEnter(Collider  collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
            characterController.IsFacingStairs = true;
        }
    }

    void OnTriggerExit(Collider  collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
            characterController.IsFacingStairs = false;
        }
    }
}
