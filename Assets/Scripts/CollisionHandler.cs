using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject _player;
    public GameObject Player => _player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _player = other.gameObject;
        }
    }
}
