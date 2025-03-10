using System;
using UnityEngine;

public class GameSceneHandler : MonoBehaviour
{
   [SerializeField] Transform _terrainPos;

   private void Start()
   {
        InitializeTerrain();
   }

    private void InitializeTerrain()
    {
        Terrain terrain = GameManager.instance.GetMapSO().Map;
        Terrain terrainObj = Instantiate(terrain, terrain.transform.position, terrain.transform.rotation , _terrainPos);
        terrainObj.gameObject.SetActive(true);
    }
}
