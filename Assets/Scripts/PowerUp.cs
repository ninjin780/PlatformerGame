using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static event Action<PowerUp> OnPowerUpCollected;
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    public float PowerUpTime = 60.0f;
    public float PowerUpForce = 6.0f;
    public Sprite EmptyPowerUp;
    public AudioSource EatingSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EatingSound.Play();
        spriteRenderer.sprite = EmptyPowerUp;
        GetComponent<Collider2D>().enabled = false;
        OnPowerUpCollected?.Invoke(this);
    }
}
