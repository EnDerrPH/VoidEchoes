using UnityEngine;

public class ReflectionTriggerHandler : MonoBehaviour
{
   [SerializeField] ReflectionHandler _reflectionHandler;
   [SerializeField] GameObject _cameraReflection;

   void OnTriggerEnter(Collider collider)
   {
        if(collider.gameObject.CompareTag("Player"))
        {
            _cameraReflection.gameObject.SetActive(true);
            _reflectionHandler.IsPlayer = true;
        }
   }

    void OnTriggerExit(Collider collider)
   {
        if(collider.gameObject.CompareTag("Player"))
        {
            _cameraReflection.gameObject.SetActive(false);
            _reflectionHandler.IsPlayer = false;
        }
   }
}
