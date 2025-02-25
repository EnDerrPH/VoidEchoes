using UnityEngine;

public class ReflectionHandler : MonoBehaviour
{
   [SerializeField] bool _isPlayer;
   GameObject _player;

   public bool IsPlayer {get => _isPlayer ; set => _isPlayer = value;}

   void Start()
   {
     _player = GameObject.FindGameObjectWithTag("Player");
   }

   void Update()
   {
     if(!_isPlayer)
     {
          return;
     }
     transform.position = new Vector3(_player.transform.position.x, transform.position.y , transform.position.z);
   }
}
