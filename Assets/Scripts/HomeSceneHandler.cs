using UnityEngine;
using UnityEngine.Events;

public class HomeSceneHandler : MonoBehaviour
{
   [SerializeField] Transform _playerPos;
   [SerializeField] Transform _shipPos;

   public UnityEvent PlayerHasSpawnEvent;

   void Start()
   {
        GameObject character = GameManager.instance.GetCharacterData().Character;
        GameObject ship = GameManager.instance.GetCharacterData().Ship;
        GameObject characterObj = Instantiate(character, _playerPos.position, character.transform.rotation);
        GameObject shipObj = Instantiate(ship, _shipPos.position, ship.transform.rotation);
        PlayerHasSpawnEvent.Invoke();
   }
}
