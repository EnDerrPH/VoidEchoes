using UnityEngine;

public class SetStairTrigger : MonoBehaviour
{
    [SerializeField] GameObject _stairTrigger;

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        _stairTrigger.SetActive(true);
    }

    public void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        _stairTrigger.SetActive(false);
    }
}
