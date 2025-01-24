using UnityEngine;

public class ReflectionTriggerHandler : MonoBehaviour
{
   [SerializeField] ReflectionHandler _reflectionHandler;

   void OnTriggerEnter(Collider collider)
   {
        if(collider.gameObject.CompareTag("Player"))
        {
            _reflectionHandler.IsPlayer = true;
        }
   }

    void OnTriggerExit(Collider collider)
   {
        if(collider.gameObject.CompareTag("Player"))
        {
            _reflectionHandler.IsPlayer = false;
        }
   }
}
