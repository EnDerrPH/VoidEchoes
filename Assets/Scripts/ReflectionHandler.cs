using UnityEngine;

public class ReflectionHandler : MonoBehaviour
{
   [SerializeField] GameObject _player;
   [SerializeField] bool _isPlayer;

   public bool IsPlayer {get => _isPlayer ; set => _isPlayer = value;}

   void Update()
   {
        if(!_isPlayer)
        {
            return;
        }
        transform.position = new Vector3(_player.transform.position.x, transform.position.y , transform.position.z);
   }
}
