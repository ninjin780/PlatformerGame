using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private List<Coin> coins;

    private void Awake()
    {
        coins = new List<Coin>();
        initCoins();
    }
    private void initCoins()
    {
        for (int i = 0; i<5;i++)
        {
        }
    }
}
