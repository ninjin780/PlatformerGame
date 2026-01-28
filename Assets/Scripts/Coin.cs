using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action<Coin> OnCoinCollected;

    public AudioSource EatingSound;
    public int Value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EatingSound.Play();
        PoolManager.BackToPool(gameObject);
        OnCoinCollected?.Invoke(this);
    }
}