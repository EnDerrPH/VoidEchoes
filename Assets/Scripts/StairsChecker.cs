using UnityEngine;

public class StairsChecker : MonoBehaviour
{
    [SerializeField] bool _bottomTrigger;

    void OnTriggerEnter(Collider  collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
            characterController.IsGoingUp = _bottomTrigger? true : false;
        }
    }
}
