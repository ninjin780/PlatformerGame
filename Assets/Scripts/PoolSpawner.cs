using System.Collections.Generic;
using UnityEngine;

//practica
public class PoolSpawner : MonoBehaviour
{
    public GameObject[] Spawners;       // Spawners for the coins
    private List<Vector2> lastPos;      // Last pos a coin is spawned
    private bool hasSpawnedBefore;      // Coin has spawned

    private void Start()
    {
        lastPos = new List<Vector2>();
        hasSpawnedBefore = false;
        SpawnNewCoin(null);
    }
    
    private void OnEnable(){
        Coin.OnCoinCollected += SpawnNewCoin;
    }
    private void OnDisable(){
        Coin.OnCoinCollected -= SpawnNewCoin;
    }

    private void SpawnNewCoin(Coin collectedCoin) {
        
        for (int i = 0; i < Spawners.Length; i++){
            
            if (collectedCoin != null) {
                lastPos.Remove(collectedCoin.transform.position);
            }        

            GameObject newCoin = PoolManager.GetObject(); // Activate coin
            if (newCoin == null) return; // Make sure coin is valid

            Vector2 newPos;

            do
            {
                newPos = Spawners[Random.Range(0, Spawners.Length)].transform.position;
            }
            while (hasSpawnedBefore && lastPos.Contains(newPos)); // Make sure its not the same pos as the last coin spawned

            newCoin.transform.position = newPos;
            lastPos.Add(newPos);
            hasSpawnedBefore = true;
        }
    }
}
