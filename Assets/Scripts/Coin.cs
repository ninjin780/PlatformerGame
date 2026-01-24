using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action<Coin> OnCoinCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PoolManager.BackToPool(gameObject);
        OnCoinCollected?.Invoke(this);
    }

}
