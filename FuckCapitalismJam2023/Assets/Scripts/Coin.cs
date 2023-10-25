using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ImpulseRandomForce, IPooledObject
{
    public void OnObjectSpawn()
    {
        Impulse();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.AddCoin();
            AudioManager.instance.Play("Coin");
            gameObject.SetActive(false);
        }
    }
}
