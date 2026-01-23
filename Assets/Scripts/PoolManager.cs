using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField]
    public GameObject Prefab;           //Coin
    public int Capacity;                //Max

    private List<GameObject> pool;
    private int counter = 0;            //Current coins ingame

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Declare pool
        pool = new List<GameObject>();

        //  Fill pool
        for (int i = 0; i < Capacity; i++)
        {
            AddNewObjectToPool();
        }
    }

    private void AddNewObjectToPool()
    {
        // Create coin
        GameObject obj = Instantiate(Prefab);
        obj.name = "Coin [" + counter.ToString("000") + "]";
        obj.SetActive(false);
        
        //Add object to list
        pool.Add(obj);
        counter++;
    }

    public static GameObject GetObject()
    {
        foreach (GameObject obj in Instance.pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

    public static void BackToPool(GameObject gameObject)
    {

        // Deactivate object
        gameObject.SetActive(false);
    }
}
