using UnityEngine;

public class GeyserDetectorHandler : MonoBehaviour
{
   [SerializeField] GeyserTrapHandler _geyserTrapHandler;
   [SerializeField] GeyserPhase _geyserPhase;

   private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _geyserTrapHandler.SetGeyserPhase(_geyserPhase);
        }
    }
}
