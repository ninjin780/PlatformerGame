using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action<Coin> OnCoinCollected;
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCoinCollected?.Invoke(this);
        // Hay que hacer que lo del object pooling se deshabilite en su posicion y aparezca en otra, esto se tiene que hacer en el spawner
    }

}
