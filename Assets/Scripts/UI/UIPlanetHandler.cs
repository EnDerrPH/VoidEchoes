using UnityEngine;

public class UIPlanetHandler : MonoBehaviour
{
   [SerializeField] float _rotationSpeed;

   void Update()
   {
        transform.Rotate(transform.rotation.x , _rotationSpeed * Time.deltaTime , transform.rotation.z);
   }
}
