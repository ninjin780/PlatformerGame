using UnityEngine;

//practica
public class PoolSpawner : MonoBehaviour
{
    public GameObject[] Spawners;       // Spawners for the coins
    private Vector2 lastPos;            // Last pos a coin is spawned
    private bool hasSpawnedBefore = false;

    private void Start()
    {
        SpawnNewCoin(null);
    }
    
    private void OnEnable(){
        Coin.OnCoinCollected += SpawnNewCoin;
    }
    private void OnDisable(){
        Coin.OnCoinCollected -= SpawnNewCoin;
    }

    private void SpawnNewCoin(Coin collectedCoin) {
        
        GameObject newCoin = PoolManager.GetObject(); // Activate coin
        if (newCoin == null) return; // Make sure coin is valid

        Vector2 newPos;

        do
        {
            newPos = Spawners[Random.Range(0, Spawners.Length)].transform.position;
        }
        while (hasSpawnedBefore && newPos == lastPos); // Make sure its not the same pos as the last coin spawned

        newCoin.transform.position = newPos;
        lastPos = newPos;
        hasSpawnedBefore = true;
    }
}
